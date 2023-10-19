using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperController : MonoBehaviour
{
    bool onGround;
    bool isJumping;
    public Rigidbody rb;
    public float jumpHeight;
    public GameObject jumper;
    public Transform jumperTr;
    Vector3 spawnPosition;
    public bool endGame = false;
    float repeatTimer;

    public PrefabSpawner prefabSpawner;
    public ScoreSystem scoreSystem;

    public Transform player;
    public float rangeToChace;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        repeatTimer = 20;
        rb = GetComponent<Rigidbody>();

        StartCoroutine(IncreaseSpawning(repeatTimer));
    }

    // Update is called once per frame
    void Update()
    {
        spawnPosition = new Vector3(Random.Range(-7, 7), 15, Random.Range(-7, 7));

        Jump();

        if (Vector3.Distance(transform.position, player.transform.position - transform.position) < rangeToChace)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        //player can jump only from the ground not in the air 
        if (onGround == true && onGround)
        {
            //audio.PlayOneShot(jumpClip);
            isJumping = true;
            rb.velocity = Vector3.up * jumpHeight;
            onGround = false;
        }

        if (onGround == true && isJumping == true)
        {
            
                rb.velocity = Vector3.up * jumpHeight;

                isJumping = false;
            
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            onGround = true;
        }
        else if (other.gameObject.CompareTag("DeadZone"))
        {
            scoreSystem.AddScore(2);

            if (prefabSpawner.endGame == true)
            {
                scoreSystem.AddScore(3);
            }
            Destroy(gameObject);
        }
    }

    void SpawnJumper()
    {
        Instantiate(jumper, spawnPosition, jumperTr.rotation);
    }

    IEnumerator IncreaseSpawning(float repeatTimer)
    {
        //yield return new WaitForSeconds(time);

        while (true)
        {
            if (repeatTimer >= 5)
            {
                repeatTimer -= 0.5f;
            }

            yield return new WaitForSeconds(repeatTimer);

            SpawnJumper();

            if (repeatTimer <= 10)
            {
                endGame = true;
            }
        }
    }
}
