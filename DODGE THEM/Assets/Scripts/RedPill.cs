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
        spawnTime = Random.Range(30, 50);
        spawnDelay = Random.Range(30, 50);

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
        newInstance = Instantiate(redPill, spawnPosition, pillTransform.rotation);

    }

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
