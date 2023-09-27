using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePill : MonoBehaviour
{
    public GameObject bluePill;
    public Transform pillTransform;
    private GameObject newIstance;

    Vector3 spawnPosition;
    float spawnTime;
    float spawnDelay;


    // Start is called before the first frame update
    void Start()
    {
        //randomizes spawn and delay times that are esential for spawning the pill
        spawnTime = Random.Range(10, 20);
        spawnDelay = Random.Range(20, 30);
        //repeats spawning 
        for (int i = 0; i < 1; i++)
        {
            InvokeRepeating("SpawnPill", spawnTime, spawnDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //setting up where pill will be respawned
        spawnPosition = new Vector3(Random.Range(-10, 10), 30, Random.Range(-10, 10));
    }

    //Pill spawner method
    public void SpawnPill()
    {
        newIstance = Instantiate(bluePill, spawnPosition, pillTransform.rotation);
    }

    // method that destroys the pill once it colledes with gameobject with specific tags
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            Destroy(bluePill);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(bluePill);
        }
    }
}

