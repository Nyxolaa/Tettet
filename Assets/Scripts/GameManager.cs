using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject spawner; 

    public static int score = 0;
    public Text scoreText;
    public GameObject gameOverText;

    public GameObject startButton;
    public GameObject stopButton;
    public GameObject replayButton;

    private bool hasGameStarted = false;
    private bool isPaused = true;

    void Start()
    {
        if (gameOverText == null)
        {
            gameOverText = GameObject.Find("GameOverText");
            if (gameOverText == null)
            {
                Debug.LogError("GameOverText objesi sahnede bulunamadı!");
            }
            else
            {
                Debug.Log("GameOverText otomatik olarak atandı.");
            }
        }

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

        if (!hasGameStarted)
        {
            spawner.GetComponent<Spawner>().SpawnNewTetromino();
            hasGameStarted = true;
        }
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
        Debug.Log("Game Over gösteriliyor.");

        gameOverText.SetActive(true);
        Time.timeScale = 0f;
        replayButton.SetActive(true);
        stopButton.SetActive(false);
    }

    public static void AddScore(int linesCleared)
    {
        Debug.Log("AddScore çağrıldı: " + linesCleared);

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
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.UpdateScoreText();
            Debug.Log("UpdateScoreText çağrıldı");
        }
        else
        {
            Debug.Log("GameManager bulunamadı");
        }

    }

    public void UpdateScoreText()
    {
        if(scoreText != null)
        {
            scoreText.text = "Skor: " + score.ToString();
            Debug.Log("Skor güncellendi: " + score);
        }
        else
        {
            Debug.LogWarning("scoreText null!");
        }
    }

}
