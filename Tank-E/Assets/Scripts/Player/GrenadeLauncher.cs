using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
    [SerializeField] Transform grenadeLauncher;
    public Rigidbody Grenade;
    public float speed = 50f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    } 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            ShootGrenade();
        }
    }
        private void ShootGrenade()
    {
        Rigidbody rbGrenade = Instantiate(Grenade, grenadeLauncher.position, grenadeLauncher.rotation);//Instancia la Granada
        rbGrenade.velocity = speed * grenadeLauncher.forward;
        
        Debug.Log("Shoot");
    }
}
