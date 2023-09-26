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
        spawnTime = Random.Range(5, 15);
        spawnDelay = Random.Range(10, 30);

        for (int i = 0; i < 1; i++)
        {
            InvokeRepeating("SpawnBox", spawnTime, spawnDelay);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnPosition = new Vector3(Random.Range(-10, 10), 30, Random.Range(-10, 10));
    }

    public void SpawnBox()
    {
        Instantiate(box, spawnPosition, boxTransform.rotation);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            scoreSystem.AddScore(1);
            Destroy(box);
        }
    }
}
