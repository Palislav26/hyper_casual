using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBigBox : MonoBehaviour
{
    public ScoreSystem scoreSystem;
    public PrefabSpawner prefabSpawner;
    public GameObject bigBox;

    //setting up what gameobjects can destroy the box + player gets 1 score for each box destroyed
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            scoreSystem.AddScore(2);

            if (prefabSpawner.endGame == true)
            {
                scoreSystem.AddScore(2);
            }
            Destroy(bigBox);
        }
    }
}
