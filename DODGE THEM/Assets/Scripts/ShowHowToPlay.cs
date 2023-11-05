using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHowToPlay : MonoBehaviour
{   
    public GameObject howToPlay;
    public GameObject playButton;
    public GameObject creditsButton;
    public GameObject howToPlayButton;
    public GameObject exitButton;

    //simply opens hidden credits UI
    public void ShowControls() 
    {
        howToPlay.SetActive(true);
        playButton.SetActive(false);
        creditsButton.SetActive(false);
        howToPlayButton.SetActive(false);
        exitButton.SetActive(false);
    }

    //closes credits UI
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Escape))
        {
            howToPlay.SetActive(false);
            playButton.SetActive(true);
            creditsButton.SetActive(true);
            howToPlayButton.SetActive(true);
            exitButton.SetActive(true);
        }
    }
}
