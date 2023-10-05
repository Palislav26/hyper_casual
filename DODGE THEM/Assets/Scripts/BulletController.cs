using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float speed = 0.05f;
    [SerializeField] GameObject bullet;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);

        DestroyBullet();
    }
    
    private void DestroyBullet()
    {
        Destroy(bullet, 5);
    }
}
