using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaziBehavour : MonoBehaviour
{
    public float rangeToChace;
    public float moveSpeed;
    public Transform player;
    public Rigidbody2D rb;
    int health;
    public int fullHealth;

    public GameObject key;
    public GameObject coin;
    public GameObject bottle;
    private int randomNum;

    float timer;
    public float rangeToAttack;
    public GameObject slash;
    public Transform attackPos;

    private float shootTimer = 0f;
    private float shootInterval = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = fullHealth;
        randomNum = Random.Range(0, 7);
    }

    // Update is called once per frame
    void Update()
    {
        KillNazi();

        if (true)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer < rangeToChace)
            {
                // Move towards the player
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

                if (distanceToPlayer < rangeToAttack)
                {
                    // Stop chasing and shoot
                    transform.position = transform.position;

                    // Update the shoot timer
                    shootTimer += Time.deltaTime;

                    if (shootTimer >= shootInterval)
                    {
                        Attack();
                        shootTimer = 0f; // Reset the timer after shooting
                    }
                }
                else
                {
                    // Player is not within shooting range, resume chasing
                    // Reset the shoot timer if needed
                }
            }
        }


    }

    /*void FixedUpdate() // try to fix this
    {
        
        if (Vector3.Distance(transform.position, player.transform.position - transform.position) < rangeToChace)
        {
            
            if (Vector3.Distance(transform.position, player.transform.position - transform.position) < rangeToAttack)
            {
                
                timer += Time.deltaTime;
                if (timer > 1f)
                {
                    Attack();
                    timer = 0;
                }
            }
            else if (Vector3.Distance(transform.position, player.transform.position - transform.position) < rangeToChace)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
            
        }       
    }*/

    void KillNazi()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= 1;
        }
    }

    void Attack()
    {
        transform.position = transform.position;
        Instantiate(slash, attackPos.position, Quaternion.identity);
    }

    private void OnDestroy()
    {
        if (randomNum == 0)
        {
            Instantiate(key, transform.position, Quaternion.identity);
        }
        else if (randomNum == 1)
        {
            Instantiate(coin, transform.position, Quaternion.identity);
        }
        else if (randomNum == 2)
        {
            Instantiate(key, transform.position, Quaternion.identity);
            Instantiate(coin, transform.position, Quaternion.identity);
        }
        else if (randomNum == 3)
        {
            Instantiate(coin, transform.position, Quaternion.identity);
            Instantiate(coin, transform.position, Quaternion.identity);
        }
        else if (randomNum == 4)
        {
            Instantiate(bottle, transform.position, Quaternion.identity);            
        }
        else if (randomNum == 5)
        {
            Instantiate(bottle, transform.position, Quaternion.identity);
            Instantiate(coin, transform.position, Quaternion.identity);
        }
        else if (randomNum == 6)
        {
            Instantiate(bottle, transform.position, Quaternion.identity);
            Instantiate(key, transform.position, Quaternion.identity);
        }
        else
        {

        }
    }
}
