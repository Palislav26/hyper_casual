using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserC : MonoBehaviour
{
    Vector3 spawnPosition;
    public GameObject laser;
    public Transform transform;
    float speed = 0.15f;

    public float secondTillDestruction = 10f;

    //public float spawnTime;
    //public float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        //generates random spawn position
        spawnPosition = new Vector3(Random.Range(30, 50), 0.58f, 12f);
        //repeats spawning method based on time that can be modified through ispector
        InvokeRepeating("RespawnLaser", Random.Range(50, 100), Random.Range(250,500));
        //repeats destroy laser method
        StartCoroutine("DestroyLaser");
    }

    // Update is called once per frame
    void Update()
    {
        //moves the laser towards player on X position
        transform.position = new Vector3(laser.transform.position.x - speed, laser.transform.position.y, laser.transform.position.z);
    }

    //spawner method for laser
    public void RespawnLaser()
    {
        Instantiate(laser, spawnPosition, laser.transform.rotation);

    }

    //destroys the laser with given time period that can be modified from the inspector
    IEnumerator DestroyLaser()
    {
        yield return new WaitForSeconds(secondTillDestruction);
        Destroy(laser);
    }
}
