using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float rangeToChace;
    public float rangeToShoot;
    public float moveSpeed;
    public Transform player;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position - transform.position) < rangeToChace)
        {            
            if (Vector3.Distance(transform.position, player.transform.position - transform.position) < rangeToShoot)
            {
                transform.position = rb.position;
            }
            else if(Vector3.Distance(transform.position, player.transform.position - transform.position) > rangeToShoot)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
        }
    }
}
