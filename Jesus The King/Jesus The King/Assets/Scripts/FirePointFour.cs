using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePointFour : MonoBehaviour
{
    public GameObject bullet;
    public GameObject firePoint4;

    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Instantiate(bullet, firePoint4.transform.position, Quaternion.identity);
        }
    }
}
