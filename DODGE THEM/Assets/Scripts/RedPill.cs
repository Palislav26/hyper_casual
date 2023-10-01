using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPill : MonoBehaviour
{
    public GameObject redPill;
    private GameObject newInstance;
    public Transform pillTransform;

    Vector3 spawnPosition;
    float spawnTime;
    float spawnDelay;


    // Start is called before the first frame update
    void Start()
    {
        //randomizes spawn and delay times that are esential for spawning the pill
        spawnTime = Random.Range(30, 50);
        spawnDelay = Random.Range(30, 50);
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
        spawnPosition = new Vector3(Random.Range(-7, 7), 15, Random.Range(-7, 7));
    }

    // spawning pill method
    public void SpawnPill()
    {
       
        newInstance = Instantiate(redPill, spawnPosition, pillTransform.rotation);
    }

    // method that destroys the pill once it colledes with gameobject with specific tags
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            Destroy(redPill);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(redPill);
        }
    }
}
