using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float radio = 10f;
    public GameObject explosionEffect;
    float countdown;
    public float damage = 25f;
    bool haExplotado = false;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;//Timer para explosi�n retardada
        if (countdown <= 0f && !haExplotado)
        {
            Explode();
            haExplotado = true;
        }
    }

    private void Explode()
    {

        Instantiate(explosionEffect, transform.position, transform.rotation);//Instanciar el efecto de explosi�n

        Collider[] hitcolliders = Physics.OverlapSphere(transform.position, radio);

        foreach (Collider col in hitcolliders)
        {
            if (col.gameObject.tag == "Enemy")
            {
                Target target = col.transform.gameObject.GetComponent<Target>();//Busca el Script donde est� la salud

                target.TakeDamage(damage);//Aplica el m�todo de da�o
            }

        }

        Destroy(gameObject, 0.6f);// delay para la destrucci�n para que instamcie la explosi�n antes de destruirse
        Debug.Log("BOOM");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}
