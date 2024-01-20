using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    float movementX;
    float movementY;
    Vector2 moveDirection;
    float vertical;
    [SerializeField] float speed;
    public ParticleSystem ps1;
    public ParticleSystem ps2;
    public ParticleSystem ps3;
    public ParticleSystem ps4;

    public GameObject f1;
    public GameObject f2;
    public GameObject f3;
    public GameObject f4;

    public int maxHealth;
    int health;

    public float maxWater = 100;
    public float currentWater;
    public WaterControl waterControl;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;

        currentWater = maxWater;
        waterControl.SetMaxWater(maxWater);
    }

    // Update is called once per frame
    void Update()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(movementX, movementY).normalized;

        ShootingParticles();
        KillPlayer();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }

    public void ShootingParticles()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {          
            if(currentWater >= 0)
            {
                ReduceWater(5);
                ps1.Play();
                f1.SetActive(true);
            }
            else
            {
                f1.SetActive(false);
            }         
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {            
            if (currentWater >= 0)
            {
                ReduceWater(5);
                ps2.Play();
                f2.SetActive(true);
            }
            else
            {
                f2.SetActive(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {            
            if (currentWater >= 0)
            {
                ReduceWater(5);
                ps3.Play();
                f3.SetActive(true);
            }
            else
            {
                f3.SetActive(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {           
            if (currentWater >= 0)
            {
                ReduceWater(5);
                ps4.Play();
                f4.SetActive(true);
            }
            else
            {
                f4.SetActive(false);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            health -= 1;
        }
        else if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            health -= 1;
        }
    }

    void KillPlayer()
    {
        if(health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void ReduceWater(float water)
    {
        currentWater -= water;
        waterControl.SetWater(currentWater);
    }
}

