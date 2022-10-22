using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerSpider : MonoBehaviour
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
    public Animator animLegs;
    public bool nonAnimatorWeapon;
    public Animator[] animWeapons;
    public GameObject[] shootVFX;
    [Header("Sounds")]
    AudioSource audioSource;
    public AudioClip shootSFX;
    public AudioClip reloadSFX;
    public AudioClip lockTargetSFX;
    public AudioClip nonTargetSFX;

    [Header("Turret")]
    public GameObject upperBody;
    public Transform sight;
    [Header("Visual Status")]
    public Light lightStatus;

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
        lightStatus.color = Color.green;
        originPosition=new Vector3(transform.position.x,transform.position.y,transform.position.z);



    }

    void Update()
    {
        if (isActive)//si se encuentra activada la unidad
        {
            animLegs.SetBool("Active", true);
            lightStatus.intensity=3;
            FindTarget();//Busca al Objetivo
            if (canAttack) Attack();//Listo para atacar si puede
        }
        else
        {
            animLegs.SetBool("Active", false);
            lightStatus.intensity=0;

        }

    }


    void FindTarget()
    {
        dist = Vector3.Distance(transform.position, target.transform.position);//distancia de visión
        if (dist < distanceTarget)
        {
            Debug.Log("See you");// :P
            MoveTowards();//Si encuenta el objetivo
            if (isLockedSound == true)//preeve que se reproduzca en loop
            {
                audioSource.PlayOneShot(lockTargetSFX);
                isLockedSound = false;
                lightStatus.color = Color.yellow;
            }
        }
        else
        {
            if (isLockedSound == false)//preeve que se reproduzca en loop
            {
                audioSource.PlayOneShot(nonTargetSFX);
                isLockedSound = true;
                lightStatus.color = Color.green;
            }
            if(isAPatrol==false)//si no patrulla
            {
                navMesh.SetDestination(originPosition);//vuelve al origen
                if(navMesh.velocity.magnitude<=0)animLegs.SetFloat("Mov",0);//vuelve a idle
            }


        }
    }


    void MoveTowards()
    {
        if(isActive==true) upperBody.transform.LookAt(target);//rota la cabeza hacia el target si esta activada la unidad
        navMesh.SetDestination(target.transform.position);//se mueve al objetivo
        animLegs.SetFloat("Mov", navMesh.velocity.magnitude);//animacion de idle a walk
    }


    void Attack()
    {
        if (dist < distanceToAttack)//comprueba si tiene rango
        {
            lightStatus.color = Color.red;
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
        if(nonAnimatorWeapon==false)foreach (Animator shootAnim in animWeapons) { shootAnim.SetTrigger("Shoot"); }//da play a todos los animators de las armas
        foreach (GameObject vfx in shootVFX) { vfx.SetActive(true); }//activa las particulas del disparo
        audioSource.pitch = (Random.Range(0.8f, 1.2f));//el audio cambia levemente el tono
        audioSource.PlayOneShot(shootSFX);//emite un sonido de disparo


        yield return new WaitForSeconds(fireRate);//espera los segundos de la variable


        if(nonReload==false)audioSource.PlayOneShot(reloadSFX);//emite un sonido de recarga
        canShoot = true;//ahora puede disparar
        foreach (GameObject vfx in shootVFX) { vfx.SetActive(false); }//desactiva las particulas del disparo

    }

}
