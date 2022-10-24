using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int totalScore;
    
    public TextMeshProUGUI scoreTXT;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreTXT.SetText("Score: "+ totalScore);
    }
    public void AddScore(int addScore)
    {
        totalScore+=addScore;

    }
}
