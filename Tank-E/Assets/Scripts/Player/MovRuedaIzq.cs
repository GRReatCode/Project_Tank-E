using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovRuedaIzq : MonoBehaviour
{
    [SerializeField] Animator ruedaizq;


    private void Start()
    {
        ruedaizq = GetComponent<Animator>();
        
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
        {
            ruedaizq.SetBool("moverAdelante", true);
        }
        else
        {
            ruedaizq.SetBool("moverAdelante", false);
        }


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S))
        {
            ruedaizq.SetBool("moverAtras", true);
        }
        else
        {
            ruedaizq.SetBool("moverAtras", false);
        }
    }
    
}
