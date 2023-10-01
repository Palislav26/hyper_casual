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

    public ScoreSystem scoreSystem;

    public float moveSpeed;
    public float rangeToChace;

    Vector3 spawnPosition;
    float spawnTime;
    float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        //reference to the enemy rigidbody
        rb = GetComponent<Rigidbody>();
        //randomizes spawn and delay values for spawning repeat method
        spawnTime = Random.Range(5, 15);
        spawnDelay = Random.Range(10, 30);
        //repeat spawning method based on randomized values
        for (int i = 0; i < 1; i++)
        {
            InvokeRepeating("SpawnEnemy", spawnTime, spawnDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //gives random position for spawning
        spawnPosition = new Vector3(Random.Range(-7, 7), 15, Random.Range(-7, 7));
        //once the player in the calculated range, enemy will start to chase him
        if (Vector3.Distance(transform.position, player.transform.position - transform.position) < rangeToChace)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }     

    }
    //spawn method
    public void SpawnEnemy()
    {
        Instantiate(enemy, spawnPosition, enemyTransform.rotation);

    }
    //once player collides with the enemy, kills it and ads 2 score for its death
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            scoreSystem.AddScore(2);
            Destroy(enemy);
        }
    }
}
