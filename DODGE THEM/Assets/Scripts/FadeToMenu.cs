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

    public void FadeToNextScene(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
