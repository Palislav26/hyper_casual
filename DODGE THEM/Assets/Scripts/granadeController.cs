using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class granadeController : MonoBehaviour
{
    public Rigidbody rb;

    public GameObject granade;
    public Transform granadeTransform;
    public MeshRenderer granadeMR;

    private float TimeToBoom;
    public float boomTimer;
    private float flashTimer;
    public float flashLenght = 0.1f;

    public float radius;
    public float explosionPower;

    public AudioSource audio;
    public AudioClip boom;

    public ParticleSystem ps;


    void Start()
    {
        //values passed from the inspector
        TimeToBoom = boomTimer;
        flashTimer = flashLenght;

    }

    // Update is called once per frame
    void Update()
    {
        //granade blicks before explodes
        if (TimeToBoom > 0)
        {
            TimeToBoom -= Time.deltaTime;
            flashTimer -= Time.deltaTime;

            if (flashTimer <= 0)
            {
                granadeMR.enabled = !granadeMR.enabled;
                flashTimer = flashLenght;
            }

            //once its at 0 timer, it explodes
            if (TimeToBoom <= 0)
            {
                ShockWave();              
                SpawnParticles();
                Destroy(granade);
            }
        }
    }

    //explosion that throws away nearby objects with colliders 
    public void ShockWave()
    {
        //assign position to the player
        Vector3 explosionPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //in radius around player all colliders are marked
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);

        //does the magic
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionPower, explosionPosition, radius, 3.0f);
                audio.PlayOneShot(boom);
            }
        }
    }

    //does exactly what you would expect
    public void SpawnParticles()
    {
        ParticleSystem newInstance = Instantiate(ps, granade.transform.position, Quaternion.identity);
        newInstance.Play();
        Destroy(newInstance, 5);
    }
}
