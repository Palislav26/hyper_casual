using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    Vector3 spawnPosition;
    public GameObject box;
    public Transform transform;

    [SerializeField] float spawnTime;
    [SerializeField] float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        box.transform.position = spawnPosition;
        spawnPosition = new Vector3(Random.Range(-10, 10), 30, Random.Range(-10, 10));
        InvokeRepeating("SpawnBox", spawnTime, spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnBox()
    {
        Instantiate(box, spawnPosition, box.transform.rotation);

    }
}
