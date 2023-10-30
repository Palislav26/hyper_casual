using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBController : MonoBehaviour
{
    Vector3 spawnPosition;
    public GameObject laser;
    public Transform transform;
    public float speed = 0.10f;
    float respawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        respawnTimer = Random.Range(25, 50);
        //generates random spawn position
        spawnPosition = new Vector3(Random.Range(30, 80), 0.7f, 1.5f);

        StartCoroutine(IncreaseSpeed(respawnTimer));
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
        GameObject newInstance = Instantiate(laser, spawnPosition, laser.transform.rotation);
        Destroy(laser, 30);
    }

    //based on repeatTimer variable increases the spawn rate over time
    IEnumerator IncreaseSpeed(float respawnTimer)
    {

        while (true)
        {
            if (speed <= 0.4f)
            {
                speed += 0.03f;
            }

            yield return new WaitForSeconds(respawnTimer);

            RespawnLaser();

        }
    }
}
