using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerTank : MonoBehaviour
{
    [SerializeField]
    [Header("Objective")]
    [Tooltip("Automatic find Player")]
    public Transform target;
    public PlayerMovement player;
    public float distanceTarget;
    public float distanceToAttack;
    [Header("Conduct")]
    public bool isAPatrol;

    public bool isActive;
    [Header("Shoot Options Fire")]
    public float fireRate;
    public bool nonReload;
    
    [Header("Animators")]
    public Animator animRoads;
    public bool nonAnimatorWeapon;
    public Animator[] animWeapons;
    public GameObject[] shootVFX;
    [Header("Sounds")]
    AudioSource audioSource;
    public AudioSource motorAudioSource;
    public AudioClip shootSFX;
    public AudioClip reloadSFX;
    public AudioClip lockTargetSFX;
    public AudioClip nonTargetSFX;

    [Header("Turret")]
    public GameObject upperBody;
    public Transform sight;

    //Variables Privadas
    NavMeshAgent navMesh;
    RaycastHit hit;
    float dist;
    bool canAttack = true;
    bool canShoot = true;
    bool isLockedSound;
    Vector3 originPosition;



    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        navMesh = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        originPosition=new Vector3(transform.position.x,transform.position.y,transform.position.z);
        motorAudioSource.volume=0.15f;
        motorAudioSource.pitch=0.5f;




    }

    void Update()
    {
        if (isActive)//si se encuentra activada la unidad
        {
            animRoads.SetBool("Active", true);
            FindTarget();//Busca al Objetivo
            if (canAttack) Attack();//Listo para atacar si puede
            motorAudioSource.enabled=true;//habilita el sonido

        }
        else
        {
            animRoads.SetBool("Active", false);
            motorAudioSource.enabled=false;

        }

    }


    void FindTarget()
    {
        dist = Vector3.Distance(transform.position, target.transform.position);//distancia de visión
        if (dist < distanceTarget)
        {
            Debug.Log("See you");// :P
            MoveTowards();//Si encuenta el objetivo
             upperBody.transform.Rotate(new Vector3(0,0,-90));//endereza el arma prefab en top
            if (isLockedSound == true)//preeve que se reproduzca en loop
            {
                audioSource.PlayOneShot(lockTargetSFX);
                isLockedSound = false;
            }
        }
        else
        {
            if (isLockedSound == false)//preeve que se reproduzca en loop
            {
                audioSource.PlayOneShot(nonTargetSFX);
                isLockedSound = true;
            }
            if(isAPatrol==false)//si no patrulla
            {
                navMesh.SetDestination(originPosition);//vuelve al origen
                if(navMesh.velocity.magnitude<=0)animRoads.SetFloat("Mov",0);//vuelve a idle
            }


        }
       
    }


    void MoveTowards()
    {
        if(isActive==true)
        {
            
            upperBody.transform.LookAt(target.position);//rota la cabeza hacia el target si esta activada la unidad

        } 
        navMesh.SetDestination(target.transform.position);//se mueve al objetivo
        animRoads.SetFloat("Mov", navMesh.velocity.magnitude);//animacion de idle a walk
        motorAudioSource.pitch=navMesh.velocity.magnitude/10;//si magnitud es 10, la dividimos y da 1 de pitch
        if(motorAudioSource.pitch<=0.5f)motorAudioSource.pitch=0.5f;// si baja de 0.5 que se mantenga
    }


    void Attack()
    {
        if (dist < distanceToAttack)//comprueba si tiene rango
        {
            Shoot();//Dispara
        }
    }


    void Shoot()
    {
        if (Physics.Raycast(sight.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))//emite un rayo
        {

            player = hit.transform.GetComponent<PlayerMovement>();//determinamos la script que busca
            if (player != null)//si la script existe
            {
                Debug.DrawLine(sight.position, target.transform.position, Color.red);//crea un rayo Gizmo rojo
                if (canShoot)//si puede disparar
                {
                    StartCoroutine("FireNow");//inica rutina de disparo
                }
            }
        }

    }
    IEnumerator FireNow()
    {
        canShoot = false;//no puede seguir disparando
        if(nonAnimatorWeapon==false) foreach (Animator shootAnim in animWeapons) { shootAnim.SetTrigger("Shoot"); }//da play a todos los animators de las armas
        foreach (GameObject vfx in shootVFX) { vfx.SetActive(true); }//activa las particulas del disparo
        audioSource.pitch = (Random.Range(0.8f, 1.2f));//el audio cambia levemente el tono
        audioSource.PlayOneShot(shootSFX);//emite un sonido de disparo


        yield return new WaitForSeconds(fireRate);//espera los segundos de la variable


        if(nonReload==false)audioSource.PlayOneShot(reloadSFX);//emite un sonido de recarga
        canShoot = true;//ahora puede disparar
        foreach (GameObject vfx in shootVFX) { vfx.SetActive(false); }//desactiva las particulas del disparo

    }

}
