using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePointOne : MonoBehaviour
{

    public GameObject bullet;
    public GameObject firePoint1;
    public Rigidbody2D bulletRB;

    // Start is called before the first frame update
    void Start()
    {
        bulletRB = bullet.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            bulletRB.velocity = Vector2.up;
            Instantiate(bullet, firePoint1.transform.position, Quaternion.identity);
        }
    }
}
