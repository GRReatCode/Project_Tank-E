using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyControllerTank : MonoBehaviour
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

    [Header("Status")]
    public bool isDead;
    public float health = 10;
    public ExploteArea exploteArea;
    public int scoreWithDeath;

    [Header("Visual FX")]
    public GameObject explosionVFX;
    public Rigidbody[] rigParts;
    public BoxCollider[] colParts;
    public GameObject[] invisibleParts;
    public GameObject[] shootVFX;
    public ShakeCamera shake;
    public Image fillAmountLife;


    //Variables Privadas
    Disappear disappear;
    ScoreManager scoreManager;
    bool addscore;
    bool isDisappear;
    NavMeshAgent navMesh;
    RaycastHit hit;
    float dist;
    bool canAttack = true;
    bool canShoot = true;
    bool canShake=true;
    bool isLockedSound;
    Vector3 originPosition;



    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        disappear = GetComponent<Disappear>();
        navMesh = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        scoreManager=FindObjectOfType<ScoreManager>();
        shake = FindObjectOfType<ShakeCamera>();
        rigParts = GetComponentsInChildren<Rigidbody>();
        colParts = GetComponentsInChildren<BoxCollider>();
        foreach (GameObject go in invisibleParts) go.SetActive(false);//desactiva trozos invisibles
        foreach (Rigidbody rb in rigParts) rb.isKinematic = true;//kinematicos los rb
        foreach (BoxCollider bc in colParts) bc.enabled = false;// desactivados los boxcolliders
        originPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        motorAudioSource.pitch = 0.5f;
        eventToStart.Invoke();




    }

    void Update()
    {

        if (health <= 0)
        {
            isDead = true;
            gameObject.tag="Untagged";
            fillAmountLife.enabled = false;
            if(addscore==false)
            {
                scoreManager.totalScore+=scoreWithDeath;
                addscore=true;

            } 

        }
        
        if (isActive && isDead == false)//si se encuentra activada la unidad
        {
            eventToActivate.Invoke();
            animRoads.SetBool("Active", true);
            FindTarget();//Busca al Objetivo
            if (canAttack) Attack();//Listo para atacar si puede
            motorAudioSource.enabled = true;//habilita el sonido
            LifeBar();
        }

        if (isActive == false)// si no se encuentra activada
        {
            animRoads.SetBool("Active", false);
            motorAudioSource.enabled = false;
        }

        if (isDead)//si esta muerta
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
            MoveTowards();//Si encuenta el objetivo
            upperBody.transform.Rotate(new Vector3(0, 0, -90));//endereza el arma prefab en top
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
            if (isAPatrol == false)//si no patrulla
            {
                navMesh.SetDestination(originPosition);//vuelve al origen
                if (navMesh.velocity.magnitude <= 0) animRoads.SetFloat("Mov", 0);//vuelve a idle
            }


        }

    }


    void MoveTowards()
    {
        if (isActive == true)
        {

            upperBody.transform.LookAt(target.position);//rota la cabeza hacia el target si esta activada la unidad

        }
        navMesh.SetDestination(target.transform.position);//se mueve al objetivo
        animRoads.SetFloat("Mov", navMesh.velocity.magnitude);//animacion de idle a walk
        motorAudioSource.pitch = navMesh.velocity.magnitude / 10;//si magnitud es 10, la dividimos y da 1 de pitch
        if (motorAudioSource.pitch <= 0.5f) motorAudioSource.pitch = 0.5f;// si baja de 0.5 que se mantenga
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
    void LifeBar()
    {
        fillAmountLife.fillAmount=health/10;


    }
}
