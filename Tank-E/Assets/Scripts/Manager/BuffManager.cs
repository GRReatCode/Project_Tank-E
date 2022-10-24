using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuffManager : MonoBehaviour
{
    [SerializeField] float spawnInterval = 3f;
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] List<Transform> enemySpawnPoints;
    [SerializeField] int minHealth = 10, maxHealth = 100;
    [SerializeField] int minSpeed = 1, maxSpeed = 10;
    [SerializeField] float attackSpeed = 1f;
    [SerializeField] bool randomSpawn = false;

    float spawnTimer; // timer for spawn interval

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnInterval; // init timer
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime; // timer countdown
        if (spawnTimer <= 0f)
        {
            if (randomSpawn)
            {
                int randEnemy = Random.Range(0, enemyPrefabs.Count);
                int randSpawnPoint = Random.Range(0, enemySpawnPoints.Count);
                Instantiate(enemyPrefabs[randEnemy], enemySpawnPoints[randSpawnPoint].position,
                    transform.rotation); // instantiate a random enemy at random position
            }
            else
            {
                Instantiate(enemyPrefabs[0], enemySpawnPoints[0].position, transform.rotation);
            }
            spawnTimer = spawnInterval; // reset spawn timer
        }
    }
}

