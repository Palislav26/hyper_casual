using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    Vector3 spawnPosition;
    public GameObject box;
    public Transform transform;

    [SerializeField] float spawnTime;
    [SerializeField] float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = new Vector3(Random.Range(-10, 10), 30, Random.Range(-10, 10));
        InvokeRepeating("SpawnBox", spawnTime, spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnBox()
    {
        Instantiate(box, spawnPosition, box.transform.rotation);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            Destroy(gameObject);
        }
    }
}
