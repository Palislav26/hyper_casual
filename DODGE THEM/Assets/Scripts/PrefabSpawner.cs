using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    Vector3 spawnPosition;
    float spawnTime1;
    float spawnTime2;
    float spawnTime3;
    float spawnTime4;
    float spawnDelay;
    public float timeToAdvance;

    public GameObject enemy;
    public Transform enemyTransform;

    // Start is called before the first frame update
    void Start()
    {
        timeToAdvance = 0;
       
        //randomizes spawn and delay values for spawning repeat method
        spawnTime1 = Random.Range(15, 30);
        spawnTime2 = Random.Range(10, 20);
        spawnTime3 = Random.Range(5, 10);
        spawnTime4 = Random.Range(2, 5);

        spawnDelay = Random.Range(10, 30);
        //repeat spawning method based on randomized values

        LateGame();
    }

    // Update is called once per frame
    void Update()
    {
        spawnPosition = new Vector3(Random.Range(-7, 7), 15, Random.Range(-7, 7));
    }

    public void LateGame() // figure this out
    {

        if (timeToAdvance <= 60)
        {
            InvokeRepeating("SpawnEnemy", spawnTime1, spawnDelay);
        }
        else if (timeToAdvance <= 120)
        {
            InvokeRepeating("SpawnEnemy", spawnTime2, spawnDelay);
        }
        else if (timeToAdvance <= 180)
        {
            InvokeRepeating("SpawnEnemy", spawnTime3, spawnDelay);
        }
        else if (timeToAdvance <= 240)
        {
            InvokeRepeating("SpawnEnemy", spawnTime4, spawnDelay);
        }
    }

    public void SpawnEnemy()
    {
        GameObject newInstance = Instantiate(enemy, spawnPosition, enemyTransform.rotation);

    }
}
