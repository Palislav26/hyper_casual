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
    [SerializeField] float enemyStrength;
    [SerializeField] float strenghtAgainstBoxes;

    public bool onGround;
    public float jumpStartTime;
    private float jumpTime;
    private bool isJumping;

    public ScoreSystem scoreSystem;
    public GameObject exitMsg;
    public ParticleSystem ps;

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

    public AudioSource audio;
    public AudioClip jumpClip;
    public AudioClip bounceClip;
    public AudioClip gunShot;
    public AudioClip reloadAmmo;
    public AudioClip emptyMagazine;
    public AudioClip outOfGranades;
    public AudioClip boom;
    public float secondsTillBoom;

    public Transform playerTR;
    Vector3 mousePos;
    Vector3 objectPos;
    float angle;

    [SerializeField] int currentAmmo;
    [SerializeField] int currentGranades;
    

    public GameObject bulletPrefab;
    public Transform firePosition;
    public GameObject granadePrefab;
    public AmmoCounter ammoCounter;


    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = 0;
        currentGranades = 0;

        //gives us reference to the rigidbody of the player
        rb = GetComponent<Rigidbody>();
        //setting up the health that is full once the game starts
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    //once the player gameobject is disabled, we must use this method to bring the player back to menu
    private void OnDisable()
    {
        PlayBoomSound();
        ps.Play();
        ShockWave();
        Invoke("LoadMenuScene", 3);
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        KillThePlayer();
        Movement();
        LookAtTheMouse();

        if (Input.GetMouseButtonDown(0) && currentAmmo > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePosition.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 10000);
            audio.PlayOneShot(gunShot);
            Destroy(bullet, 5);
            currentAmmo -= 1;
            ammoCounter.DeductBullets(1);
        }
        else if(Input.GetMouseButtonDown(0) && currentAmmo <= 0)
        {
            audio.PlayOneShot(emptyMagazine);
        }
        else if(Input.GetMouseButtonDown(1) && currentGranades > 0)
        {
            GameObject granade = Instantiate(granadePrefab, firePosition.position, Quaternion.identity);
            granade.GetComponent<Rigidbody>().AddForce(transform.forward * 3000);
           
            currentGranades -= 1;
            ammoCounter.DeductGranades(1);
            PlayBoomSound();
            secondsTillBoom = 0;
        }
        else if (Input.GetMouseButtonDown(1) && currentGranades <= 0)
        {
            audio.PlayOneShot(outOfGranades);
        }


        ps.transform.position = transform.position;

        //once player presses ESC the game pauses and exit screen pops up 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            exitMsg.SetActive(true);
        }    

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

        //esential for playing boom sound // fix this as well
        if (secondsTillBoom > 0)
        {
            secondsTillBoom -= Time.deltaTime;
        }
    }

    void Movement()
    {
        //setting up vertical and horizontal movement
        verticalMovement = Input.GetAxis("Vertical");
        horizontalMovement = Input.GetAxis("Horizontal");

        rb.velocity = new Vector3(horizontalMovement * speed, rb.velocity.y, verticalMovement * speed);
    }

    void Jump()
    {
        //player can jump only from the ground not in the air 
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            audio.PlayOneShot(jumpClip);
            isJumping = true;
            jumpTime = jumpStartTime;
            rb.velocity = Vector3.up * jumpHeight;
            onGround = false;         
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {

            if (jumpTime > 0)
            {
                rb.velocity = Vector3.up * jumpHeight;
                jumpTime -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
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
            //gets rigidbody of the enemy once player touch it
            Rigidbody boxRigidbody = other.gameObject.GetComponent<Rigidbody>();
            //gives the direction - away from the player
            Vector3 awayFromPlayer = transform.position - other.gameObject.transform.position;
            //pushes enemy away from the player with calculated direction
            boxRigidbody.AddForce(awayFromPlayer * strenghtAgainstBoxes, ForceMode.Impulse);
        }
        else if (other.gameObject.CompareTag("BigBox"))
        {
            onGround = true;
            //gets rigidbody of the enemy once player touch it
            Rigidbody boxRigidbody = other.gameObject.GetComponent<Rigidbody>();
            //gives the direction - away from the player
            Vector3 awayFromPlayer = transform.position - other.gameObject.transform.position;
            //pushes enemy away from the player with calculated direction
            boxRigidbody.AddForce(awayFromPlayer * strenghtAgainstBoxes, ForceMode.Impulse);
        }
        else if(other.gameObject.CompareTag("DeadZone"))
        {
            TakeDamage(5);
            //if player is under immortality timer, this will "kill" him instead
            gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Laser"))
        {
            TakeDamage(1);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
            audio.PlayOneShot(bounceClip);
            //gets rigidbody of the enemy once player touch it
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            //gives the direction - away from the player
            Vector3 awayFromPlayer = transform.position - other.gameObject.transform.position;
            //pushes enemy away from the player with calculated direction
            enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);

            //gives the direction player - opposite direction from the enemy
            //Vector3 awayFromEnemy = -transform.position + other.gameObject.transform.position;
            //pushes player to this direction
            //rb.AddForce(awayFromEnemy * enemyStrength, ForceMode.Impulse);
        }
        else if (other.gameObject.CompareTag("BluePill"))
        {
            //ShockWave();
            //ps.Play();
            audio.PlayOneShot(reloadAmmo);
            currentGranades += 1;
        }
        else if (other.gameObject.CompareTag("RedPill"))
        {
            //once player collects the redpill, pill regenerates one health point to the healthbar of the player
            currentHealth += 1;
            healthBar.SetHealth(currentHealth);
        }
        else if (other.gameObject.CompareTag("Ammo"))
        {
            audio.PlayOneShot(reloadAmmo); 
            
            currentAmmo += 25;
            
        }
    }

    //creates explosion around the player and throws all gameobjects with colliders away
    public void ShockWave()
    {
        //assign position to the player
        Vector3 explosionPosition = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
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

    void LookAtTheMouse()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 5.23f; //The distance between the camera and object
        objectPos = Camera.main.WorldToScreenPoint(playerTR.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        playerTR.rotation = Quaternion.Euler(new Vector3(0, -angle + 90, 0));
    }

    void PlayBoomSound() // fix this
    {             
        if (secondsTillBoom <= 0)
        {
            audio.PlayOneShot(boom);
            secondsTillBoom = 2f;
        }
    }

    void LoadMenuScene( )
    {
        SceneManager.LoadScene(0);
    }
}
