using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    Rigidbody rb;
    float verticalMovement;
    float horizontalMovement;
    //SF allows us to modify these two variables direcly throgh inspector
    [SerializeField]float speed = 5f;
    [SerializeField]float jumpHeight = 10f;
    [SerializeField]float normalStrength;

    public bool onGround;

    public ScoreSystem scoreSystem;
    public GameObject exitMsg;

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

    // Start is called before the first frame update
    void Start()
    {
        //gives us reference to the rigidbody of the player
        rb = GetComponent<Rigidbody>();
        //setting up the health that is full once the game starts
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    //once the player gameobject is disabled, we must use this method to bring the player back to menu
    private void OnDisable()
    {       
        SceneManager.LoadScene(0);                                                       
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
        }
        //once player presses ESC the game pauses and exit screen pops up 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            exitMsg.SetActive(true);
        }

        //calling KTP method
        KillThePlayer();

        /*once the player is hit by anything, he becomes invincible for time that can be specified through inspector
         * player mesh rendered within this period flashes from disabled to active and in reverse
        */
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

    //player takes damage with passed value
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

    //not really kills him, just disable him as gameobject, shochwave effect will trigger
    void KillThePlayer()
    {
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
            ShockWave();
        }
    }

    //detecs if player collides with other gameObjects based on their tag
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

            //gets rigidbody of the enemy once player touch it
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            //gives the direction - away from the player
            Vector3 awayFromPlayer = transform.position - other.gameObject.transform.position;
            //pushes enemy away from the player with calculated direction
            enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);

            //gives the direction player - opposite direction from the enemy
            Vector3 awayFromEnemy = other.gameObject.transform.position - transform.position;
            //pushes player to this direction
            rb.AddForce(awayFromEnemy * normalStrength, ForceMode.Impulse);
        }
        else if (other.gameObject.CompareTag("BluePill"))
        {
            ShockWave();
        }
        else if (other.gameObject.CompareTag("RedPill"))
        {
            //once player collects the redpill, pill regenerates one health point to the healthbar of the player
            currentHealth += 1;
            healthBar.SetHealth(currentHealth);
        }
    }

    //creates explosion around the player and throws all gameobjects with colliders away
    public void ShockWave()
    {
        //assign position to the player
        Vector3 explosionPosition = transform.position;
        //in radius around player all colliders are marked
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);

        //does the magic
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
