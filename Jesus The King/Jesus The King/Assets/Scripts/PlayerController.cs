using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public TMP_Text coinText;
    public TMP_Text keyText;
    private int coinsNum;
    private int keysNum;

    public int maxHealth;
    int health;

    public float maxWater = 100;
    public float currentWater;
    public WaterControl waterControl;

    public bool hasKey = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;

        currentWater = maxWater;
        waterControl.SetMaxWater(maxWater);

        coinText.text = "";
        keyText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(movementX, movementY).normalized;

        ShootingParticles();
        KillPlayer();

        coinText.text = "" + coinsNum;
        keyText.text = "" + keysNum;

        if(keysNum > 0)
        {
            hasKey = true;
        }
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
        else if (collision.gameObject.CompareTag("Key"))
        {
            keysNum = keysNum + 1;
        }
        else if (collision.gameObject.CompareTag("Coin"))
        {
            coinsNum = coinsNum + 1;
        }
        else if (collision.gameObject.CompareTag("LockedWall"))
        {
            if(keysNum == 0)
            {

            }
            else
            {
                keysNum = keysNum - 1;
            }           
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

