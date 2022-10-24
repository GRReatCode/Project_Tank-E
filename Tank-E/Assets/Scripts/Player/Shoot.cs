using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float damage = 20f;
    public Transform Cannon;
    public float fireRate = 1f;
    public GameObject shootEffect;
    RaycastHit objectHit;
    AudioSource audioSource;
    public AudioClip shootSFX;
    bool canShoot=true;


    // Start is called before the first frame update
    void Start()
    {
        audioSource=GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        /*if (canShoot)
        {
            if (Input.GetMouseButtonUp(0)) ShootBullet();
        }*/
        if (canShoot)
        {
            if (Input.GetMouseButtonUp(0)) StartCoroutine("FireRate");;
        }

    }
    void ShootBullet()
    {
        //ShootEffect.Play();
        RaycastHit hit;
        if (Physics.Raycast(Cannon.transform.position, Cannon.transform.forward, out hit)) ; //Emite el Raycast
        {
            Debug.DrawRay(Cannon.transform.position, Cannon.transform.forward * 100, Color.red);
            StartCoroutine("FireRate");
            /*if (hit.collider == null)
            {
                return;
            }
            else if (hit.collider.CompareTag("Enemy"))
            {
                Target target = hit.transform.GetComponent<Target>();
                target.TakeDamage(damage);  //Aplicar da�o
            }*/

        }
    }
    IEnumerator FireRate()
    {
        canShoot = false;
        shootEffect.SetActive(true);
        audioSource.PlayOneShot(shootSFX);

        yield return new WaitForSeconds(fireRate);
        shootEffect.SetActive(false);
        canShoot = true;
    }
}
