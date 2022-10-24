using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float hMove, vMove;
    Animator anim;
    public AudioSource audioSource;
    public AudioClip motor;
    Rigidbody rb;
    [SerializeField] float velocidad;
    [SerializeField] float velocidadGiro;

    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
        rb=GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
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
}
