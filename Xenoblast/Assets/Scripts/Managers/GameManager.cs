using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;
    public int score;

    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        score = 0;

        // Ensures there is only one GameManager
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        AudioManager.Instance.PlayMusic("GameTheme");
    }

    private void Start()
    {
        UpdateScoreUI();    
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int amount)
    {
        score = amount;
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

}
