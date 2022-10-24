using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToDesactivate : MonoBehaviour
{
    public float timer;
    float initTimer;
    bool stopCount;
    public GameObject[] toDesactivate;
    void OnEnable()
    {
        stopCount=false;
        initTimer=timer;
    }


    // Update is called once per frame
    void Update()
    {
        timer-=Time.deltaTime;
        if(timer<=0 && stopCount==false)
        {
            stopCount=true;
            foreach (GameObject go in toDesactivate)
            {
                go.SetActive(false);
            }
            timer=initTimer;
            

        }
    }
}
