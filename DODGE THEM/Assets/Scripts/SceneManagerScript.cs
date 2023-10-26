using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{

    public GameObject ExitMsg;
    public bool playPressed = false;

    public void Play()
    {
        //SceneManager.LoadScene(1);
        playPressed = true;
    }

    public void CloseApp()
    {
        Application.Quit();
    }

    public void Continue()
    {
        //Unpause the game and closes exit screen UI
        Time.timeScale = 1;
        ExitMsg.SetActive(false);
    }
}
