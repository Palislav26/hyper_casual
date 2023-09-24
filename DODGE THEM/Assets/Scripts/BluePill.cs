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
        spawnTime = Random.Range(10, 20);
        spawnDelay = Random.Range(20, 30);

        for (int i = 0; i < 1; i++)
        {
            InvokeRepeating("SpawnPill", spawnTime, spawnDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnPosition = new Vector3(Random.Range(-10, 10), 30, Random.Range(-10, 10));
    }

    public void SpawnPill()
    {
        newIstance = Instantiate(bluePill, spawnPosition, pillTransform.rotation);
    }

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

