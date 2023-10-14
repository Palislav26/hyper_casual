using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    public GameObject ammo;
    private GameObject newInstance;
    public Transform ammoTransform;

    Vector3 spawnPosition;
    float spawnTime;
    float spawnDelay;

    public AmmoCounter ammoCounter;

    //public AudioSource audio;
    //public AudioClip healClip;

    // Start is called before the first frame update
    void Start()
    {
        //randomizes spawn and delay times that are esential for spawning the pill
        spawnTime = Random.Range(30, 50);
        spawnDelay = Random.Range(30, 50);
        //repeats spawning 
        for (int i = 0; i < 1; i++)
        {
            InvokeRepeating("SpawnAmmo", spawnTime, spawnDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //setting up where pill will be respawned
        spawnPosition = new Vector3(Random.Range(-7, 7), 15, Random.Range(-7, 7));

    }

    // spawning pill method
    public void SpawnAmmo()
    {

        newInstance = Instantiate(ammo, spawnPosition, ammoTransform.rotation);
        //Destroy(newInstance, 10);
    }

    // method that destroys the pill once it colledes with gameobject with specific tags
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            Destroy(ammo);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            //audio.PlayOneShot(healClip);
            ammoCounter.AddBullets(25);
            Destroy(ammo);
        }
    }
}
