using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;

    [SerializeField] float bounceForce;

    public float moveSpeed;
    public float rangeToChace;
    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position - transform.position) < rangeToChace)
        {
            moveDirection = player.transform.position - transform.position;
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        moveDirection.Normalize();
        rb.velocity = moveDirection * moveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Rigidbody otherRB = collision.rigidbody;
            rb.AddExplosionForce(bounceForce, collision.contacts[0].point, 5);
        }
    }
}
