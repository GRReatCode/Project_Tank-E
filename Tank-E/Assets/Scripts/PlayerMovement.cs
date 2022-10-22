using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    ShakeCamera shakeCamera;

    void Start()
    {
        shakeCamera= GameObject.FindObjectOfType<ShakeCamera>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Impact"))
        {
            Debug.Log("oh");
            shakeCamera.timeToShake=0.5f;
            shakeCamera.shake=true;
            
        }
    }
}
