using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaziBehavour : MonoBehaviour
{
    public float rangeToChace;
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
                transform.position = rb.position;
                      
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);            
        }
    }
}
