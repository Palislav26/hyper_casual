using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    Vector3 spawnPosition;

    float repeatTimer;

    public GameObject enemy;
    public Transform enemyTransform;

    // Start is called before the first frame update
    void Start()
    {
        repeatTimer = 20;

        StartCoroutine(IncreaseSpawning(repeatTimer));
    }

    // Update is called once per frame
    void Update()
    {
        spawnPosition = new Vector3(Random.Range(-7, 7), 15, Random.Range(-7, 7));            
    }

    public void SpawnEnemy()
    {
        GameObject newInstance = Instantiate(enemy, spawnPosition, enemyTransform.rotation);
    }

    IEnumerator IncreaseSpawning(float repeatTimer)
    {
        //yield return new WaitForSeconds(time);
       
        while(true)
        {
            if (repeatTimer >= 2) 
            {
                repeatTimer -= 0.5f;
            }           
            
            yield return new WaitForSeconds(repeatTimer);
            SpawnEnemy();
        }
        
    }
}
