using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovRuedaDerecha : MonoBehaviour
{
    [SerializeField] Animator ruedaDer;


    private void Start()
    {
        ruedaDer = GetComponent<Animator>();
        
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            ruedaDer.SetBool("moverAtras", true);
        }
        else
        {
            ruedaDer.SetBool("moverAtras", false);
        }


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W))
        {
            ruedaDer.SetBool("moverAdelante", true);
        }
        else
        {
            ruedaDer.SetBool("moverAdelante", false);
        }
    }
    
}
