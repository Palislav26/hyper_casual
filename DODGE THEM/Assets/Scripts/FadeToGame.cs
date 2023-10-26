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
        if (sceneMS.playPressed == true)
        {
            FadeToNextScene(1);
        }
    }
    public void FadeToNextScene(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("MenuFadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
