using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeToMenu : MonoBehaviour
{
    public Animator animator;
    public PlayerController playerController;

    private int levelToLoad;

    // Update is called once per frame
    void Update()
    {
        if(playerController.currentHealth <= 0)
        {
            FadeToNextScene(0);
        }       
    }

    //once player is at 0 hp, trigger fade in animation
    public void FadeToNextScene(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    //Once fade in animation is complete, we are transformed to main menu
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
