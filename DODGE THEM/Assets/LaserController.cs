using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
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
        spawnPosition = new Vector3(-3, 0.5f, -30);
        InvokeRepeating("RespawnLaser", spawnTime, spawnDelay);
        StartCoroutine("DestroyLaser");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + speed);
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
