using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    Vector3 spawnPosition;
    public GameObject laser;  
    public Transform transform;
    float speed = 0.15f;

    public float secondTillDestruction = 10f;

    //[SerializeField]float spawnTime;
    //[SerializeField]float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        //generates random spawn position
        spawnPosition = new Vector3(-3, 0.5f, Random.Range(-30, -80));
        //repeats spawning method based on time that can be modified through ispector
        InvokeRepeating("SpawnLaser", Random.Range(10, 20), Random.Range(50, 100));
        //repeats destroy laser method
        StartCoroutine("DestroyLaser");
    }

    // Update is called once per frame
    void Update()
    {
        //moves the laser towards player on Z position
        transform.position = new Vector3(laser.transform.position.x, laser.transform.position.y, laser.transform.position.z + speed);
    }

    //spawner method for laser
    public void SpawnLaser()
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
