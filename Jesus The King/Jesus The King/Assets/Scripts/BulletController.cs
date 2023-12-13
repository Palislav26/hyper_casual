using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float timeToVanish;
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        coroutine = Vanish(timeToVanish); 
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.right;
    }

    private IEnumerator Vanish(float timeToVanish)
    {
        float vanishTimer = timeToVanish;
        vanishTimer -= Time.deltaTime;

        yield return new WaitForSeconds(timeToVanish);

        if (vanishTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
