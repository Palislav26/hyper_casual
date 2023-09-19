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

    /*private void Awake()
    {
        instance = this;
    }*/

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Laser"))
        {
            gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = transform.position - other.gameObject.transform.position;

            enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);

            Vector3 awayFromEnemy = other.gameObject.transform.position - transform.position;

            rb.AddForce(awayFromEnemy * normalStrength, ForceMode.Impulse);
        }
    }
}
