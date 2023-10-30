using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bullet;

    Vector3 spawnPosition;
    public Transform spawnerTR;
    public Transform playerTr;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //setting up position that is same as the players
        spawnerTR = playerTr;       
        spawnPosition = new Vector3(spawnerTR.position.x, spawnerTR.position.y, spawnerTR.position.z);

        if (Input.GetMouseButtonDown(0))
        {
            SpawnBullet();
        }
    }

    //spawn method
    public void SpawnBullet()
    {
        Instantiate(bullet, spawnPosition, Quaternion.identity);

    }
}
