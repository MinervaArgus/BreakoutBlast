using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
}
