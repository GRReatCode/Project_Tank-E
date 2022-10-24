using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeCamera : MonoBehaviour
{
    CinemachineVirtualCamera vcam;
    CinemachineBasicMultiChannelPerlin noise;
    public float amplitudeGain, frequencyGain, timeToShake;
    public bool shake;

    void Start()
    {
        vcam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        shake=false;
    }
    void Update()
    {
        if(shake)
        {
            StartCoroutine ("ShakeNow");

        }
        
    }
    IEnumerator ShakeNow()
    {
        shake=false;
        noise.m_AmplitudeGain = amplitudeGain;
        noise.m_FrequencyGain = frequencyGain;
        yield return new WaitForSeconds (timeToShake);
        shake=false;
        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;
    }

    public void Noise(float amplitudeGain, float frequencyGain)
    {
        noise.m_AmplitudeGain = amplitudeGain;
        noise.m_FrequencyGain = frequencyGain;
    }
}
