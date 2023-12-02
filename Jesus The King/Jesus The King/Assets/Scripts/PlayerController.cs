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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(movementX, movementY).normalized;

        Shooting();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }

    public void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ps1.Play();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            ps2.Play();
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            ps3.Play();
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            ps4.Play();
        }

    }
}

