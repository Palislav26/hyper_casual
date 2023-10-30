using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeToGame : MonoBehaviour
{
    public Animator animator;
    public SceneManagerScript sceneMS;

    private int levelToLoad;

    // Update is called once per frame
    void Update()
    {
        //once the play button is pressed it calls FadeToNExtScene method
        if (sceneMS.playPressed == true)
        {
            FadeToNextScene(1);
        }
    }
    //This method will trigger MenuFadeOut animation
    public void FadeToNextScene(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("MenuFadeOut");
    }
    //Once FadeOut animation is done, we are in the game playing
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
