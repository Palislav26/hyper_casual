using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Rigidbody rb;
    public Transform player;

    public GameObject enemy;
    public Transform enemyTransform;

    public float moveSpeed;
    public float rangeToChace;

    Vector3 spawnPosition;
    float spawnTime;
    float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        spawnTime = Random.Range(5, 15);
        spawnDelay = Random.Range(10, 30);

        for (int i = 0; i < 1; i++)
        {
            InvokeRepeating("SpawnEnemy", spawnTime, spawnDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {

        spawnPosition = new Vector3(Random.Range(-10, 10), 30, Random.Range(-10, 10));

        if (Vector3.Distance(transform.position, player.transform.position - transform.position) < rangeToChace)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }     

    }

    public void SpawnEnemy()
    {
        Instantiate(enemy, spawnPosition, enemyTransform.rotation);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            Destroy(enemy);
        }
    }
}
