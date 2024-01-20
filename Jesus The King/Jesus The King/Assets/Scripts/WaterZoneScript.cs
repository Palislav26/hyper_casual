using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterZoneScript : MonoBehaviour
{
    public PlayerController playerController;

    IEnumerator RestoreWater()
    {     
        for(float currentWater = playerController.currentWater; currentWater < 100; currentWater += 0.005f)
        {
            playerController.currentWater = currentWater;
        }

        yield return new WaitForSeconds(Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Jesus") && playerController.currentWater < 100)
        {
            StartCoroutine("RestoreWater");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Jesus"))
        {
            StopCoroutine("RestoreWater");
        }
    }
}
