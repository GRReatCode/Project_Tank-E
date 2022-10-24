using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPitch : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip audioClip;
    public float min,max;
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        audioSource.pitch=Random.Range(min,max);
        audioSource.PlayOneShot(audioClip);
    }
}
