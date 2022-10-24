using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimeManager : MonoBehaviour
{
    public float timeCount;
    float timePast;
    public TextMeshProUGUI timeTXT;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCount-=Time.deltaTime;


        timeTXT.SetText("Time Bonus: "+ Mathf.Round(timeCount));
        if(timeCount<=0)
        {
            timeTXT.SetText("Time Bonus Out");
            timeTXT.color=Color.red;
            timeCount=0;

        }

        
    }
}
