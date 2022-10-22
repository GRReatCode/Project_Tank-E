using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class ExploteArea : MonoBehaviour
{
    public bool exploteNow;

    public float radius = 5;
    public float forceExplosion = 1000;
    AudioSource audioSource;
    public AudioClip explosionSFX;
    bool soundPlayed;
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        
    }


    void Update()
    {
        if (exploteNow) Explote();
    }
    public void ExplosionNow(bool boomNow)
    {
        boomNow = exploteNow;

    }
    void Explote()
    {
        if (soundPlayed == false)
        {
            audioSource.pitch = Random.Range(0.6f, 1.4f);
            audioSource.PlayOneShot(explosionSFX);
            soundPlayed = true;

        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(forceExplosion, transform.position, radius);
            }

        }
        Destroy(this);


    }
}
