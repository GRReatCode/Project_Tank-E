using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuffController : MonoBehaviour
{
    PlayerMove player;
    AudioSource audioSource;
    public AudioClip healthBuffSFX, doubleMachinegunSFX, multiplegunSFX,speedSFX;
    public GameObject healthBuff, doubleMachinegun, multiplegun,speedVFX;
    public ParticleSystem doubleMachinegunVFX;
    float timeRemain;
    public TextMeshProUGUI bufferTXT;

    bool newBuff;
    void Start()
    {
        player=FindObjectOfType<PlayerMove>();
        audioSource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(newBuff)
        {
            timeRemain-=Time.deltaTime;

        }
        if(timeRemain<=0)
        {
            newBuff=false;
            timeRemain=0; 
            bufferTXT.SetText("No Buff");
            healthBuff.SetActive(false);
            doubleMachinegun.SetActive(false);
            multiplegun.SetActive(false);
            speedVFX.SetActive(false);
            player.velocidad=25;
        }
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("HealthBuff"))
        {
            StartCoroutine("TextEffect");
            player.health+=50;
            audioSource.PlayOneShot(healthBuffSFX);
            other.GetComponent<Collider>().enabled=false;
            healthBuff.SetActive(true);
            timeRemain=3;
            newBuff=true;
            bufferTXT.SetText("Health Buff");
            Destroy(other.gameObject, 0.2f);
        }
        if(other.CompareTag("DoubleMachinegunBuff"))
        {
            StartCoroutine("TextEffect");
            audioSource.PlayOneShot(doubleMachinegunSFX);
            other.GetComponent<Collider>().enabled=false;
            doubleMachinegunVFX.Play();
            doubleMachinegun.SetActive(true);
            timeRemain=15;
            newBuff=true;
            bufferTXT.SetText("Double Machinegun");
            Destroy(other.gameObject, 0.2f);
        }
        if(other.CompareTag("MultiplegunBuff"))
        {
            StartCoroutine("TextEffect");
            audioSource.PlayOneShot(multiplegunSFX);
            other.GetComponent<Collider>().enabled=false;
            doubleMachinegunVFX.Play();
            multiplegun.SetActive(true);
            timeRemain=15;
            newBuff=true;
            bufferTXT.SetText("Multiplegun");
            Destroy(other.gameObject, 0.2f);
        }
        if(other.CompareTag("SpeedBuff"))
        {
            StartCoroutine("TextEffect");
            audioSource.PlayOneShot(speedSFX);
            other.GetComponent<Collider>().enabled=false;
            player.velocidad=40;
            speedVFX.SetActive(true);
            timeRemain=15;
            newBuff=true;
            bufferTXT.SetText("More Speed");
            Destroy(other.gameObject, 0.2f);
        }
    }
    IEnumerator TextEffect()
    {
        bufferTXT.color=Color.red;
        bufferTXT.fontSize=bufferTXT.fontSize*2;
        yield return new WaitForSeconds(0.3f);
        bufferTXT.color=Color.white;
        bufferTXT.fontSize=bufferTXT.fontSize/2;
    }
}
