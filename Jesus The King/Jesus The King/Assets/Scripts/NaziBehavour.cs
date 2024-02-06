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
    public int rangeToAttack;
    public GameObject slash;
    public Transform attackPos;

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
    }

    void FixedUpdate()
    {

        if (Vector3.Distance(transform.position, player.transform.position - transform.position) < rangeToChace)
        {
            if (Vector3.Distance(transform.position, player.transform.position - transform.position) < rangeToAttack)
            {
                transform.position = rb.position;

                timer += Time.deltaTime;
                if (timer > 1f)
                {
                    timer = 0;
                    Attack();
                }
            }
            else if (Vector3.Distance(transform.position, player.transform.position - transform.position) > rangeToAttack)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, player.transform.position - transform.position) < rangeToAttack)
                {
                    transform.position = rb.position;

                    timer += Time.deltaTime;
                    if (timer > 1f)
                    {
                        timer = 0;
                        Attack();
                    }
                }
            }
        }
    }

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
