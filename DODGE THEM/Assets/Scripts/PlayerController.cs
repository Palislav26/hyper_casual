using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //declaring esential variables
    //public static PlayerController instance;
    Rigidbody rb;
    float verticalMovement;
    float horizontalMovement;
    //SF allows us to modify these two variables direcly throgh inspector
    [SerializeField]float speed = 5f;
    [SerializeField]float jumpHeight = 10f;
    [SerializeField]float normalStrength;

    public bool onGround;

    public ScoreSystem scoreSystem;

    public float radius;
    public float explosionPower;

    public int maxHealth = 5;
    public int currentHealth;
    public HealthBar healthBar;

    private float immortalityTimer;
    public float immortalityLenght;
    public MeshRenderer playerMR;
    private float flashTimer;
    public float flashLenght = 0.1f;

    /*private void Awake()
    {
        instance = this;
    }*/

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //setting up vertical and horizontal movement
        verticalMovement = Input.GetAxis("Vertical");
        horizontalMovement = Input.GetAxis("Horizontal");

        rb.velocity = new Vector3(horizontalMovement * speed, rb.velocity.y, verticalMovement * speed);

        //player can jump only from the ground not in the air
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
            onGround = false;
            scoreSystem.AddScore(1);
        }

        KillThePlayer();

        if(immortalityTimer > 0)
        {
            immortalityTimer -= Time.deltaTime;
            flashTimer -= Time.deltaTime;

            if(flashTimer <= 0)
            {
                playerMR.enabled = !playerMR.enabled;
                flashTimer = flashLenght;
            }

            if(immortalityTimer <= 0)
            {
                playerMR.enabled = true;
            }

        }
    }

    void TakeDamage(int damage)
    {       
        if(immortalityTimer <= 0)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            immortalityTimer = immortalityLenght;

            playerMR.enabled = false;
            flashTimer = flashLenght;
        }
                                 
    }

    void KillThePlayer()
    {
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
            ShockWave();
        }
    }

    //detecs if player collides with the platform
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            onGround = true;
        }
        else if (other.gameObject.CompareTag("Box"))
        {
            onGround = true;
        }
        else if(other.gameObject.CompareTag("DeadZone"))
        {
            TakeDamage(5);
        }
        else if (other.gameObject.CompareTag("Laser"))
        {
            TakeDamage(1);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);

            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = transform.position - other.gameObject.transform.position;

            enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);

            Vector3 awayFromEnemy = other.gameObject.transform.position - transform.position;

            rb.AddForce(awayFromEnemy * normalStrength, ForceMode.Impulse);
        }
        else if (other.gameObject.CompareTag("BluePill"))
        {
            ShockWave();
            //Destroy(bluePill);
        }
        else if (other.gameObject.CompareTag("RedPill"))
        {
            currentHealth += 1;
            healthBar.SetHealth(currentHealth);
            //Destroy(redPill);
        }
    }

    public void ShockWave()
    {
        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);

        foreach(Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionPower, explosionPosition, radius, 3.0f);
            }
        }
    }
}
