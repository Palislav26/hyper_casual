using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBController : MonoBehaviour
{
    Vector3 spawnPosition;
    public GameObject laser;
    public Transform transform;
    float speed = 0.05f;

    public float secondTillDestruction = 10f;

    public float spawnTime;
    public float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = new Vector3(Random.Range(30, 80), 0.7f, 1.5f);
        InvokeRepeating("RespawnLaser", spawnTime, spawnDelay);
        StartCoroutine("DestroyLaser");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(laser.transform.position.x - speed, laser.transform.position.y, laser.transform.position.z);
    }

    public void RespawnLaser()
    {
        Instantiate(laser, spawnPosition, laser.transform.rotation);

    }

    IEnumerator DestroyLaser()
    {
        yield return new WaitForSeconds(secondTillDestruction);
        Destroy(laser);
    }
}
