using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePointOne : MonoBehaviour
{

    public GameObject bullet;
    public GameObject firePoint1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Instantiate(bullet, firePoint1.transform.position, Quaternion.identity);
        }
    }
}
