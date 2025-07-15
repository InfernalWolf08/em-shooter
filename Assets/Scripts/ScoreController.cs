using System;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public int score;
    public int highScore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
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

    public void winMsg(string asset)
    {
        // Get highscore
        highScore = PlayerPrefs.GetInt("HighScore");
        if (score>highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        // Display text
        winText.text = $"\nYou made it out alive{asset}\n\nFinal Score: {score}\nHighscore: {highScore}";
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
        loseText.text = $"\nYou have DIED\n\nFinal Score: {score}\nHighscore: {highScore}";
    }
}