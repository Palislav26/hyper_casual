using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public GameObject box;
    public Transform boxTransform;

    public ScoreSystem scoreSystem;

    Vector3 spawnPosition;
    float spawnTime;
    float spawnDelay;


    // Start is called before the first frame update
    void Start()
    {
        //randomizes spawn and delay times that are esential for spawning the box
        spawnTime = Random.Range(5, 15);
        spawnDelay = Random.Range(10, 30);
        //repeats spawning
        for (int i = 0; i < 1; i++)
        {
            InvokeRepeating("SpawnBox", spawnTime, spawnDelay);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //setting up where pill will be respawned
        spawnPosition = new Vector3(Random.Range(-7, 7), 15, Random.Range(-7, 7));
    }

    //spawns box
    public void SpawnBox()
    {
        Instantiate(box, spawnPosition, boxTransform.rotation);

    }

    //setting up what gameobjects can destroy the box + player gets 1 score for each box destroyed
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            scoreSystem.AddScore(1);
            Destroy(box);
        }
    }
}
