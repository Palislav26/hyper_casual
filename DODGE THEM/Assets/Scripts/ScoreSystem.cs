using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text highScore;

    public int score;
    

    // Start is called before the first frame update
    void Start()
    {
        //every game score starts at zero
        score = 0;
        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }

    //method that increases the value of the score
    public void AddScore(int newScore)
    {
        score += newScore;       
    }

    //method that reflects the currect score on the screen
    public void UpdateScore()
    {
        scoreText.text = "Score " + score;

        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            //highest score is saved and kept till player tops it again 
            PlayerPrefs.SetInt("HighScore", score);
            highScore.text = score.ToString();
        }
    }
}
