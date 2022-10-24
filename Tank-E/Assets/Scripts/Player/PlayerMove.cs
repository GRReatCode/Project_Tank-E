using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    float hMove, vMove;
    Animator anim;
    public Collider mainCollider;
    public AudioSource audioSource;
    public AudioClip motor;
    Rigidbody rb;
     public Rigidbody[] rigParts;
    public BoxCollider[] colParts;
    public GameObject[] invisibleParts;
    public GameObject explosionVFX;

    public float health;
    public float maxHealth=100;
    public TextMeshProUGUI healthTXT;
    public float velocidad;
    public float velocidadGiro;

    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
        rb=GetComponent<Rigidbody>();
        rigParts = GetComponentsInChildren<Rigidbody>();
        colParts = GetComponentsInChildren<BoxCollider>();
        foreach (GameObject go in invisibleParts) go.SetActive(false);//desactiva trozos invisibles
        foreach (Rigidbody rb in rigParts) rb.isKinematic = true;//kinematicos los rb
        foreach (BoxCollider bc in colParts) bc.enabled = false;// desactivados los boxcolliders
        mainCollider.enabled=true;
        health=maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        healthTXT.SetText("Health: "+Mathf.Round(health));

        Movimiento();
        if(health<=0)Death();
        if(health>100)health=100;
    }

    void Movimiento()
    {

        vMove = Input.GetAxis("Vertical") * velocidad * Time.deltaTime;
        hMove = Input.GetAxis("Horizontal") * velocidad * velocidadGiro * Time.deltaTime;
        
        audioSource.pitch=0.9f;
        if(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.D))
        {
            audioSource.pitch++;
            anim.SetFloat("Mov", 1);
        }
        if(Input.GetKeyUp(KeyCode.W)||Input.GetKeyUp(KeyCode.A)||Input.GetKeyUp(KeyCode.S)||Input.GetKeyUp(KeyCode.D))audioSource.pitch--;
        if(audioSource.pitch<=0.8f)audioSource.pitch=0.8f;
        if(audioSource.pitch>=1.1f)audioSource.pitch=1.1f;

        transform.Rotate(0, hMove, 0);
        transform.Translate(0, 0, vMove);
    }
    void Death()
    {
        explosionVFX.SetActive(true);
        foreach (GameObject go in invisibleParts) go.SetActive(true);//activa trozos invisibles
        foreach (Rigidbody rb in rigParts) rb.isKinematic = false;//dejan de ser kinematic las partes
        foreach (BoxCollider bc in colParts) bc.enabled = true;//los colliders se activan
    }
    
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Impact"))health--;
    }
}
