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

    //public ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        //setting up values based on passed values in inspector
        TimeToBoom = boomTimer;
        flashTimer = flashLenght;

    }

    // Update is called once per frame
    void Update()
    {
        if (TimeToBoom > 0)
        {
            TimeToBoom -= Time.deltaTime;
            flashTimer -= Time.deltaTime;

            if (flashTimer <= 0)
            {
                granadeMR.enabled = !granadeMR.enabled;
                flashTimer = flashLenght;
            }

            if (TimeToBoom <= 0)
            {
                ShockWave();
                audio.PlayOneShot(boom);
                //ps.Play();
                Destroy(granade);
            }
        }
    }

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
            }
        }
    }

}
