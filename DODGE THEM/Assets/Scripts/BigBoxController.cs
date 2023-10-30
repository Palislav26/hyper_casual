using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoxController : MonoBehaviour
{
    public GameObject bigBox;
    public Transform boxTransform;

    Vector3 spawnPosition;

    float repeatTimer;


    // Start is called before the first frame update
    void Start()
    {
        repeatTimer = 50;

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
    public void SpawnBigBox()
    {
        GameObject newInstance = Instantiate(bigBox, spawnPosition, boxTransform.rotation);

    }

    //this method spawns more BigBoxes over time based on passed repeatTimer variable
    IEnumerator IncreaseSpawning(float repeatTimer)
    {

        while (true)
        {
            if (repeatTimer >= 31)
            {
                repeatTimer -= 1f;
            }

            yield return new WaitForSeconds(repeatTimer);

            SpawnBigBox();
        }
    }
}
