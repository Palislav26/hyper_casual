using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCredits : MonoBehaviour
{
    public GameObject credits;
    public GameObject playButton;
    public GameObject creditsButton;
    public GameObject exitButton;

    public void StartCredits()
    {
        credits.SetActive(true);
        playButton.SetActive(false);
        creditsButton.SetActive(false);
        exitButton.SetActive(false);
    }


     void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Escape))
        {
            credits.SetActive(false);
            playButton.SetActive(true);
            creditsButton.SetActive(true);
            exitButton.SetActive(true);
        }
    }
}