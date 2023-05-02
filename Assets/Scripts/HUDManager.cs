using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/*
 * Jackson Nevins
 * HUDManager.cs
 * Controls the Heads Up Display while the game is being played
 */

public class HUDManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject gameHUD;
    public GameObject gameOverScreen;
    public TMP_Text scoreText;

    private int score;

    private void Start()
    {
        gameHUD.SetActive(true);
    }


    public void GameOver()
    {
        gameHUD.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    public void IncreaseScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + (score);
    }

    public int GetScore()
    {
        string scoreStr = scoreText.text.Replace("Score: ", "").Trim();
        int score;
        if (int.TryParse(scoreStr, out score))
        {
            return score;
        }
        else
        {
            Debug.LogError("Failed to parse score text");
            return 0;
        }
    }
}
