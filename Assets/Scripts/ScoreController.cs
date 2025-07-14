using System;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public int score;
    public int highScore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI loseText;

    void Start()
    {
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
    }

    void Update()
    {
        scoreText.text = "Score: " + System.Convert.ToString(score);
    }

    public void displayScore()
    {
        // Get highscore
        highScore = PlayerPrefs.GetInt("HighScore");
        if (score>highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        // Display text
        loseText.text = $"\nYou Have Died\n\nFinal Score: {score}\nHighscore: {highScore}";
    }
}