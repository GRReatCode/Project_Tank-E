using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float damage = 20f;
    public Transform Cannon;
    public float fireRate = 15f;
    //public GameObject ShootEffect;
    RaycastHit objectHit;

    private float nextShoot = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && Time.time >= nextShoot)
        {
            nextShoot = Time.time + 1f / fireRate;
            ShootBullet();
        }
    }
    void ShootBullet()
    {
        //ShootEffect.Play();
        RaycastHit hit;
        if (Physics.Raycast(Cannon.transform.position, Cannon.transform.forward, out hit)); //Emite el Raycast
        {
            Debug.DrawRay(Cannon.transform.position, Cannon.transform.forward * 100, Color.red);
            //Instantiate(ShootEffect, Cannon.position, Cannon.rotation);
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();//Busca si el enemigo tiene el script de salud
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
