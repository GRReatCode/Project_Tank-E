using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
    public GameObject rootParent;
    public bool disappearNow;
    public float timeToDisappear;
    public float timeToDestroy;

    public Collider[] allColliders;
    public Rigidbody[] allRigidbodys;


    bool active;
    

    void Start()
    {
        allColliders = rootParent.gameObject.GetComponentsInChildren<Collider>();
        allRigidbodys = rootParent.gameObject.GetComponentsInChildren<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (disappearNow)
        {
            timeToDisappear -= Time.deltaTime;
            if (timeToDisappear <= 0)
            {
                active = true;
            }

        }

        if (active == true)
        {
            ActiveDisappear();
            Destroy(rootParent,timeToDestroy);
        } 
    }
    void ActiveDisappear()
    {
        foreach (Rigidbody rb in allRigidbodys) rb.isKinematic = false;
        foreach (Collider col in allColliders) col.enabled = false;
    }
    public void DisappearNow(bool disappear)
    {
        disappear = disappearNow;
    }
}
