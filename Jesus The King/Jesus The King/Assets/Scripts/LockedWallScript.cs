using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedWallScript : MonoBehaviour
{
    public GameObject darkZone;
    public PlayerController playerController;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerController.hasKey)
        {
            gameObject.SetActive(false);
            darkZone.SetActive(false);

            playerController.hasKey = false;
        }
    }
}
