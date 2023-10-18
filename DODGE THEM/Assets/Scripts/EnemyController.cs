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
    public PrefabSpawner prefabSpawner;

    public float moveSpeed;
    public float rangeToChace;

    Vector3 spawnPosition;

    public float normalStrength;

    // Start is called before the first frame update
    void Start()
    {
        //reference to the enemy rigidbody
        rb = GetComponent<Rigidbody>();
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {

            scoreSystem.AddScore(2);

            if (prefabSpawner.endGame == true)
            {
                scoreSystem.AddScore(3);
            }
            Destroy(enemy);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = other.gameObject.GetComponent<Rigidbody>();
            //gives the direction - away from the player
            Vector3 awayFromEnemy = transform.position - other.gameObject.transform.position;
            //pushes enemy away from the player with calculated direction
            playerRigidbody.AddForce(awayFromEnemy.normalized * normalStrength, ForceMode.Impulse);
            //playerRigidbody.velocity = awayFromEnemy * Mathf.Max(500f, 0);
        }
    }
}
