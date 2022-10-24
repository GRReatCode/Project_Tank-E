using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public TextMeshProUGUI countEnemiesTXT;
    public int enemiesNumber;
    int enemycount;
    void Start()
    {
        

        
    }

    // Update is called once per frame
    void Update()
    {
        enemycount=enemiesNumber-1;
        enemiesNumber=GameObject.FindGameObjectsWithTag("Enemy").Length;
        countEnemiesTXT.SetText("Bots Left: " + enemiesNumber);
        if(enemiesNumber<=enemycount)StartCoroutine("TextEffect");
    }
    IEnumerator TextEffect()
    {
        countEnemiesTXT.color=Color.red;
        countEnemiesTXT.fontSize=countEnemiesTXT.fontSize*2;
        yield return new WaitForSeconds(0.1f);
        countEnemiesTXT.color=Color.white;
        countEnemiesTXT.fontSize=countEnemiesTXT.fontSize/2;
    }
}
