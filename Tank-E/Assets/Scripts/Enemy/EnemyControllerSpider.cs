using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyControllerSpider : MonoBehaviour
{
    [SerializeField]
    [Header("Event with Start")]
    public UnityEvent eventToStart;
    [Header("Event with Activate")]
    public UnityEvent eventToActivate;
    [Header("Event with Dead")]
    public UnityEvent eventToDeath;
    [Header("Objective")]
    [Tooltip("Automatic find Player")]
    public Transform target;
    //public PlayerMove player;
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

    [Header("Sounds")]
    AudioSource audioSource;
    public AudioClip shootSFX;
    public AudioClip reloadSFX;
    public AudioClip lockTargetSFX;
    public AudioClip nonTargetSFX;

    [Header("Turret")]
    public GameObject upperBody;
    public Transform sight;
    [Header("Status")]
    public Light lightStatus;
    public bool isDead;
    public ExploteArea exploteArea;
    public float health = 100;
    [Header("Visual FX")]
    public GameObject explosionVFX;
    public Rigidbody[] rigParts;
    public BoxCollider[] colParts;
    public GameObject[] invisibleParts;
    public GameObject[] shootVFX;
    public ShakeCamera shake;


    //Variables Privadas
    Disappear disappear;
    bool isDisappear;
    NavMeshAgent navMesh;
    RaycastHit hit;
    float dist;
    bool canAttack = true;
    bool canShoot = true;
    bool canShake = true;
    bool isLockedSound;
    Vector3 originPosition;



    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        disappear = GetComponent<Disappear>();
        navMesh = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        shake = FindObjectOfType<ShakeCamera>();
        rigParts = GetComponentsInChildren<Rigidbody>();
        colParts = GetComponentsInChildren<BoxCollider>();
        foreach (GameObject go in invisibleParts) go.SetActive(false);//desactiva trozos invisibles
        foreach (Rigidbody rb in rigParts) rb.isKinematic = true;//kinematicos los rb
        foreach (BoxCollider bc in colParts) bc.enabled = false;// desactivados los boxcolliders
        lightStatus.color = Color.green;//color status
        originPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);//lugar de origen
        eventToStart.Invoke();



    }

    void Update()
    {
        if (health <= 0) isDead = true;
        if (isActive && isDead == false)//si se encuentra activada la unidad
        {
            animLegs.SetBool("Active", true);
            lightStatus.intensity = 3;
            FindTarget();//Busca al Objetivo
            if (canAttack) Attack();//Listo para atacar si puede
            eventToActivate.Invoke();
        }

        if (isActive == false)// si no se encuentra activada
        {
            animLegs.SetBool("Active", false);
            lightStatus.intensity = 0;
        }

        if (isDead == true)//si esta muerta
        {
            eventToDeath.Invoke();
            disappear.disappearNow = true;//activar desaparecer
            isActive = false;//desactiva el robot
            if (canShake)
            {
                shake.timeToShake = 1;// establece tiempo de vibracion de la camara
                shake.shake = true;// vibra la camara
                canShake = false;
            }


            explosionVFX.SetActive(true);//activa particulas
            exploteArea.exploteNow = true;//explota


            if (isDisappear == false)//comprueba si aun no desaparecieron los objetos
            {
                foreach (GameObject go in invisibleParts) go.SetActive(true);//activa trozos invisibles
                foreach (Rigidbody rb in rigParts) rb.isKinematic = false;//dejan de ser kinematic las partes
                foreach (BoxCollider bc in colParts) bc.enabled = true;//los colliders se activan
                isDisappear = true;//deja de comprobar
            }

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
            if (isAPatrol == false)//si no patrulla
            {
                navMesh.SetDestination(originPosition);//vuelve al origen
                if (navMesh.velocity.magnitude <= 0) animLegs.SetFloat("Mov", 0);//vuelve a idle
            }

        }
    }

    void MoveTowards()
    {
        if (isActive == true) upperBody.transform.LookAt(target);//rota la cabeza hacia el target si esta activada la unidad
        navMesh.SetDestination(target.transform.position);//se mueve al objetivo
        animLegs.SetFloat("Mov", navMesh.velocity.magnitude);//animacion de idle a walk
    }

    void Attack()
    {
        if (dist < distanceToAttack)//comprueba si tiene rango
        {
            /*if (Physics.Raycast(sight.position, transform.TransformDirection(Vector3.forward), out hit,))//emite un rayo
            {
                Debug.DrawLine(sight.position, transform.TransformDirection(Vector3.forward));
                player = hit.transform.GetComponent<PlayerMovement>();//determinamos si el rayo colisiona con player
                if (player==null)canShoot = false;
                else
                {
                    canAttack=true;
                    StartCoroutine("FireNow");//inica rutina de disparo
                } 
            }*/
            if (canShoot)//si puede disparar
            {
                StartCoroutine("FireNow");//inica rutina de disparo
            }
        }
    }

    IEnumerator FireNow()
    {
        canShoot = false;//no puede seguir disparando
        if (nonAnimatorWeapon == false) foreach (Animator shootAnim in animWeapons) { shootAnim.SetTrigger("Shoot"); }//da play a todos los animators de las armas
        foreach (GameObject vfx in shootVFX) { vfx.SetActive(true); }//activa las particulas del disparo
        audioSource.pitch = (Random.Range(0.8f, 1.2f));//el audio cambia levemente el tono
        audioSource.PlayOneShot(shootSFX);//emite un sonido de disparo


        yield return new WaitForSeconds(fireRate);//espera los segundos de la variable


        if (nonReload == false) audioSource.PlayOneShot(reloadSFX);//emite un sonido de recarga
        canShoot = true;//ahora puede disparar
        foreach (GameObject vfx in shootVFX) { vfx.SetActive(false); }//desactiva las particulas del disparo

    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BulletPlayer"))
        {
            health--;
        }
    }

}
