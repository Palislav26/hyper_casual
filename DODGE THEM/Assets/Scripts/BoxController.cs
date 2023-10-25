using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public GameObject box;
    public Transform boxTransform;

    Vector3 spawnPosition;

    float repeatTimer;


    // Start is called before the first frame update
    void Start()
    {
        repeatTimer = 8;
        //repeats spawning
        StartCoroutine(IncreaseSpawning(repeatTimer));
        
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
        GameObject newInstance = Instantiate(box, spawnPosition, boxTransform.rotation);

    }   

    IEnumerator IncreaseSpawning(float repeatTimer)
    {
        //yield return new WaitForSeconds(time);

        while (true)
        {
            if (repeatTimer >= 2)
            {
                repeatTimer -= 0.2f;
            }

            yield return new WaitForSeconds(repeatTimer);

            SpawnBox();
        }
    }
}
