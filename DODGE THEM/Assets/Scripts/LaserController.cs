using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    Vector3 spawnPosition;
    public GameObject laser;  
    public Transform transform;
    float speed = 0.03f;

    public float secondTillDestruction = 10f;

    [SerializeField]float spawnTime;
    [SerializeField]float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = new Vector3(-3, 0.5f, Random.Range(-30, -80));
        InvokeRepeating("SpawnLaser", spawnTime, spawnDelay);
        StartCoroutine("DestroyLaser");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(laser.transform.position.x, laser.transform.position.y, laser.transform.position.z + speed);
    }

    public void SpawnLaser()
    {
        Instantiate(laser, spawnPosition, laser.transform.rotation);

    }

    IEnumerator DestroyLaser()
    {
        yield return new WaitForSeconds(secondTillDestruction);
        Destroy(laser);
    }
}
