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
    public AudioClip gunSwitch;
    public AudioClip shotGunShot;

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

    public GameObject gun;
    public GameObject shotGun;
    public GameObject megaBulletPrefab;
    public bool gunEquiped = true;
    public bool shotGunEquiped = false;


    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = 50;
        currentGranades = 1;

        //gives us reference to the rigidbody of the player
        rb = GetComponent<Rigidbody>();
        //setting up the health that is full once the game starts
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    //once the player gameobject is disabled (player dies), we can still execute some methods here
    private void OnDisable()
    {
        PlayBoomSound();
        ps.Play();
        ShockWave();
    }

    // Update is called once per frame
    void Update()
    {
        //we call all method here
        Jump();
        KillThePlayer();
        Movement();
        LookAtTheMouse();

        //once player presses left mouse button, and has any ammo left, he shoots
        if (Input.GetMouseButtonDown(0) && currentAmmo > 0)
        {
            if(gunEquiped == true)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePosition.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 10000);
                audio.PlayOneShot(gunShot);
                Destroy(bullet, 5);
                currentAmmo -= 1;
                ammoCounter.DeductBullets(1);
            }
            else if(shotGunEquiped == true && currentAmmo > 1) //shotgun - same logic
            {
                GameObject megaBullet = Instantiate(megaBulletPrefab, firePosition.position, Quaternion.identity);
                GameObject megaBullet1 = Instantiate(megaBulletPrefab, firePosition.position, Quaternion.identity);
                GameObject megaBullet2 = Instantiate(megaBulletPrefab, firePosition.position, Quaternion.identity);
                megaBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 20000);
                megaBullet1.GetComponent<Rigidbody>().AddForce(transform.forward * 20000);
                megaBullet2.GetComponent<Rigidbody>().AddForce(transform.forward * 20000);
                audio.PlayOneShot(shotGunShot);
                Destroy(megaBullet, 0.2f);
                Destroy(megaBullet1, 0.2f);
                Destroy(megaBullet2, 0.2f);
                currentAmmo -= 2;
                ammoCounter.DeductBullets(2);
            }
            else
            {
                audio.PlayOneShot(emptyMagazine);
            }
            
        }
        else if(Input.GetMouseButtonDown(0) && currentAmmo <= 0) //once the player tries to shoot and has no ammo, makes sound of empty magazine
        {
            audio.PlayOneShot(emptyMagazine);
        }
        else if(Input.GetMouseButtonDown(1) && currentGranades > 0) //once player presses right mouse button, and has any granades left, he shoots one
        {
            GameObject granade = Instantiate(granadePrefab, firePosition.position, Quaternion.identity);
            granade.GetComponent<Rigidbody>().AddForce(transform.forward * 3000);
           
            currentGranades -= 1;
            ammoCounter.DeductGranades(1);
            Invoke("PlayBoomSound", 2);
        }
        else if (Input.GetMouseButtonDown(1) && currentGranades <= 0) // //once the player tries to shoot and has no granades, makes sound of empty granade launcher
        {
            audio.PlayOneShot(outOfGranades);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1)) // switching weapons
        {
            gun.SetActive(true);
            shotGun.SetActive(false);
            gunEquiped = true;
            shotGunEquiped = false;
            audio.PlayOneShot(gunSwitch);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // switching weapons
        {
            gun.SetActive(false);
            shotGun.SetActive(true);
            gunEquiped = false;
            shotGunEquiped = true;
            audio.PlayOneShot(gunSwitch);
        }

        //assigning particles position with position of the player
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
    }

    //setting up vertical and horizontal movement
    void Movement()
    {      
        verticalMovement = Input.GetAxis("Vertical");
        horizontalMovement = Input.GetAxis("Horizontal");

        rb.velocity = new Vector3(horizontalMovement * speed, rb.velocity.y, verticalMovement * speed);
    }

    //Jumping
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

        //player can calculate their jump, longer the imput is, longer the jump is
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
        else if (other.gameObject.CompareTag("water"))
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
            
            currentAmmo += 50;
            
        }
        else if (other.gameObject.CompareTag("Jumper"))
        {
            TakeDamage(1);
            audio.PlayOneShot(bounceClip);
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

    //player always looks at the mouse position
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

    //there was an error if I tried to play boom sound without this method..
    void PlayBoomSound() 
    {             
            audio.PlayOneShot(boom);    
    }
}
