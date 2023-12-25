using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePointThree : MonoBehaviour
{
    public GameObject bullet;
    public GameObject firePoint3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Instantiate(bullet, firePoint3.transform.position, Quaternion.identity);
        }
    }
}
