using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //declaring esential variables
    Rigidbody rb;
    float verticalMovement;
    float horizontalMovement;
    //SF allows us to modify these two variables direcly throgh inspector
    [SerializeField]float speed = 5f;
    [SerializeField]float jumpHeight = 10f;

    public bool onGround;

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
        }
    }

    //detecs if player collides with the platform
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onGround = true;
        }
        else if (collision.gameObject.CompareTag("Box"))
        {
            onGround = true;
        }
        else if (collision.gameObject.CompareTag("Laser"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("DeadZone"))
        {
            Destroy(gameObject);
        }
    }
}
