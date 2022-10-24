using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimeManager : MonoBehaviour
{
    public float timeCount;
    float timePast;
    float initialTime;
    public bool starCount;
    public bool addingScore;
    public TextMeshProUGUI timeTXT;
    ScoreManager score;
    void Start()
    {
        initialTime=timeCount;
        score=FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(starCount==true)CountDown();
        else timeCount=initialTime;
   

  
    }
    public void CountDown()
    {
        timeCount-=Time.deltaTime;
        timeTXT.SetText("Time Bonus: "+ Mathf.Round(timeCount));
        if(timeCount<=0)
        {
            starCount=false;
            timeTXT.SetText("Time Bonus Out");
            timeTXT.color=Color.red;
            timeCount=0;

        }
        else
        {
            timeTXT.color=Color.white;

        }
    }
    public void AddTimeScore()
    {
        starCount=false;
        score.totalScore+=Mathf.RoundToInt(timeCount);
        StartCoroutine ("ResetTimer");
    }
    IEnumerator ResetTimer()
    {
        yield return new WaitForSeconds(3f);
        starCount=true;
    }
}
