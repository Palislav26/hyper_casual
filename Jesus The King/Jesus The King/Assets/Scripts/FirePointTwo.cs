using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePointTwo : MonoBehaviour
{
    public GameObject bullet;
    public GameObject firePoint2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Instantiate(bullet, firePoint2.transform.position, Quaternion.identity);
        }
    }
}
