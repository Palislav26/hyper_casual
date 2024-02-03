using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningDoors : MonoBehaviour
{
    public PlayerController player;
    public GameObject openDoors;
    public GameObject closedDoors;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && player.hasKey)
        {           
               closedDoors.SetActive(false);                            
        }
    }

    private void OnDisable()
    {
        openDoors.SetActive(true);
    }
}
