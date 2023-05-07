using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text highestScoreText;

    int score;

    public void UpdateScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highestScoreText.text = "Highest: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        }

    }

    public void UpdateHighestScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            if (PlayerPrefs.GetInt("HighScore") < this.score)
            {
                PlayerPrefs.SetInt("HighScore", this.score);
            }
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", this.score);
        }
    }
}
