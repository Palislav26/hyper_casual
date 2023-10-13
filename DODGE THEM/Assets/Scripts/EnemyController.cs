using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    float spawnTime1;
    float spawnTime2;
    float spawnTime3;
    float spawnTime4;
    float spawnDelay;

    public float normalStrength;

    // Start is called before the first frame update
    void Start()
    {

        //reference to the enemy rigidbody
        rb = GetComponent<Rigidbody>();
        //randomizes spawn and delay values for spawning repeat method
        spawnTime1 = Random.Range(15, 30);
        spawnTime2 = Random.Range(10, 20);
        spawnTime3 = Random.Range(5, 10);
        spawnTime4 = Random.Range(2, 5);

        spawnDelay = Random.Range(10, 30);
        //repeat spawning method based on randomized values

        //LateGame();

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
        GameObject newInstance = Instantiate(enemy, spawnPosition, enemyTransform.rotation);

    }
    //once player collides with the enemy, kills it and ads 2 score for its death
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            scoreSystem.AddScore(2);
            Destroy(enemy);
        }
        /*else if (other.gameObject.CompareTag("Player"))
        {
            //gets rigidbody of the player 
            Rigidbody playerRigidbody = other.gameObject.GetComponent<Rigidbody>();
            //gives the direction - away from the player
            Vector3 awayFromPlayer = transform.position - other.gameObject.transform.position;
            //pushes player away from the player with calculated direction
            playerRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
        }*/
    }

    /*private void LateGame()
    {       
        if(timeToAdvance <= 60)
        {
            InvokeRepeating("SpawnEnemy", spawnTime1, spawnDelay);
        }
        else if(timeToAdvance <= 120)
        {
            InvokeRepeating("SpawnEnemy", spawnTime2, spawnDelay);
        }
        else if(timeToAdvance <= 180)
        {
            InvokeRepeating("SpawnEnemy", spawnTime3, spawnDelay);
        }
        else if(timeToAdvance <= 240)
        {
            InvokeRepeating("SpawnEnemy", spawnTime4, spawnDelay);
        }
    }*/
}
