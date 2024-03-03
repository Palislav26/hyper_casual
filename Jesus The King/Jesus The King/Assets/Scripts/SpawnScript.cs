using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject[] spawners = new GameObject[6];

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        foreach(GameObject spawn in spawners)
        {
            int num = Random.Range(0, 6);

            Instantiate(enemy, spawners[num].transform.position, Quaternion.identity);           
        }
    }  
}
