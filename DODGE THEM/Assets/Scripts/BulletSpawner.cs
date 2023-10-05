using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletSpawnerTransform;

    Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        spawnPosition = new Vector3(bulletSpawnerTransform.position.x, bulletSpawnerTransform.position.y, bulletSpawnerTransform.position.z);

        if (Input.GetMouseButtonDown(0))
        {
            SpawnBullet();
        }
    }

    public void SpawnBullet()
    {
        Instantiate(bullet, spawnPosition, bullet.transform.rotation);

    }
}
