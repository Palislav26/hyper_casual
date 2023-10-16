using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberController : MonoBehaviour
{
    public Rigidbody rb;
    public Transform player;

    public GameObject bomber;
    public Transform bomberTransform;
    public MeshRenderer bomberMR;

    public ScoreSystem scoreSystem;

    public float moveSpeed;
    public float rangeToChace;
    private float TimeToBoom;
    public float boomTimer;
    private float flashTimer;
    public float flashLenght = 0.1f;

    public float radius;
    public float explosionPower;
    public ParticleSystem ps;

    Vector3 spawnPosition;
    float spawnTime;
    float spawnDelay;

    public AudioSource audio;
    public AudioClip boom;

    // Start is called before the first frame update
    void Start()
    {
        //setting up values based on passed values in inspector
        TimeToBoom = boomTimer;
        flashTimer = flashLenght;

        //reference to the enemy rigidbody
        rb = GetComponent<Rigidbody>();
        //randomizes spawn and delay values for spawning repeat method
        spawnTime = Random.Range(25, 50);
        spawnDelay = Random.Range(50, 70);
        //repeat spawning method based on randomized values
        for (int i = 0; i < 1; i++)
        {
            InvokeRepeating("SpawnBomber", spawnDelay, spawnTime);
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

            //once bomber gets close enough to player, flashes and within couple of second explodes
            if (TimeToBoom > 0)
            {
                TimeToBoom -= Time.deltaTime;
                flashTimer -= Time.deltaTime;

                if (flashTimer <= 0)
                {
                    bomberMR.enabled = !bomberMR.enabled;
                    flashTimer = flashLenght;
                }

                if (TimeToBoom <= 0)
                {
                    ShockWave();
                    audio.PlayOneShot(boom);
                    SpawnParticles();
                    Destroy(bomber);
                }
            }
        }
    }
    //spawn method
    public void SpawnBomber()
    {
        Instantiate(bomber, spawnPosition, bomberTransform.rotation);

    }
    //once player collides with the enemy, kills it and ads 2 score for its death
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            //scoreSystem.AddScore(2);
            Destroy(bomber);
        }
    }

    //creates explosion around the player and throws all gameobjects with colliders away
    public void ShockWave()
    {
        //assign position to the player
        Vector3 explosionPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //in radius around player all colliders are marked
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);

        //does the magic
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionPower, explosionPosition, radius, 3.0f);                
            }
        }
    }

    public void SpawnParticles()
    {
        ParticleSystem newInstance = Instantiate(ps, bomber.transform.position, Quaternion.identity);
        newInstance.Play();
        Destroy(newInstance, 5);

    }
}
