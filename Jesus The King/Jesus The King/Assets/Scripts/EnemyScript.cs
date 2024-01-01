using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float rangeToChace;
    public float rangeToShoot;
    public float moveSpeed;
    public Transform player;
    public Rigidbody2D rb;
    int health;
    public int fullHealth;

    public GameObject bullet;
    public Transform shootPos;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = fullHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, player.transform.position - transform.position) < rangeToChace)
        {            
            if (Vector3.Distance(transform.position, player.transform.position - transform.position) < rangeToShoot)
            {
                transform.position = rb.position;

                timer += Time.deltaTime;
                if (timer > 2)
                {
                    timer = 0;
                    shoot();
                }
            }
            else if(Vector3.Distance(transform.position, player.transform.position - transform.position) > rangeToShoot)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
        }

        KillEnemy();
       
    }

    void shoot()
    {
        Instantiate(bullet, shootPos.position, Quaternion.identity);
    }

    void KillEnemy()
    {
        if (health <= 0)
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
}
