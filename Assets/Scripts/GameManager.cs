using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int score = 0;
    public Text scoreText;
    public GameObject gameOverText;

    public GameObject startButton;
    public GameObject stopButton;
    public GameObject replayButton;

    private bool isPaused = true;

    void Start()
    {
        UpdateScoreText();

        Time.timeScale = 0f;
        gameOverText.SetActive(false);
        replayButton.SetActive(false);
        stopButton.SetActive(false);
    }

public void StartGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        startButton.SetActive(false);
        stopButton.SetActive(true);
    }

    public void StopGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        stopButton.SetActive(false);
        startButton.SetActive(true);
    }

    public void ReplayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowGameOver()
    {
        gameOverText.SetActive(true);
        Time.timeScale = 0f;
        replayButton.SetActive(true);
        stopButton.SetActive(false);
    }

    public static void AddScore(int linesCleared)
    {
        int points = 0;
        switch (linesCleared)
        {
            case 1: points = 100; break;
            case 2: points = 300; break;
            case 3: points = 500; break;
            case 4: points = 800; break;
        }

        score += points;

        // GameManager sahnedeki objeyse, skor güncellemesini yapalım
        if (FindObjectOfType<GameManager>() != null)
        {
            FindObjectOfType<GameManager>().UpdateScoreText();
        }
    }

    public void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Skor: " + score.ToString();
    }



}
