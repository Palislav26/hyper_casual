using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    Vector3 spawnPosition;
    public GameObject laser;  
    public Transform transform;
    float speed = 0.10f;
    float respawnTimer;

    //[SerializeField]float spawnTime;
    //[SerializeField]float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        respawnTimer = Random.Range(25, 50);
        //generates random spawn position
        spawnPosition = new Vector3(-3, 0.5f, Random.Range(-30, -80));
        //repeats spawning method based on time that can be modified through ispector
        //InvokeRepeating("SpawnLaser", Random.Range(10, 20), Random.Range(50, 100));
        //repeats destroy laser method
        StartCoroutine(IncreaseSpeed(respawnTimer));
    }

    // Update is called once per frame
    void Update()
    {
        //moves the laser towards player on Z position
        transform.position = new Vector3(laser.transform.position.x, laser.transform.position.y, laser.transform.position.z + speed);
    }

    //spawner method for laser
    public void RespawnLaser()
    {
        GameObject newInstance = Instantiate(laser, spawnPosition, laser.transform.rotation);
        Destroy(laser, 30);

    }

    //destroys the laser with given time period that can be modified from the inspector
    IEnumerator IncreaseSpeed(float respawnTimer)
    {

        while (true)
        {
            if (speed <= 0.5f)
            {
                speed += 0.05f;
            }

            yield return new WaitForSeconds(respawnTimer);

            RespawnLaser();

        }
    }
}
