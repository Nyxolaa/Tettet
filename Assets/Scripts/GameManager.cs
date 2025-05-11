using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int score = 0;
    public Text scoreText;

    void Start()
    {
        UpdateScoreText();
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
