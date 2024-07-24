using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI keyText;
    public TextMeshProUGUI quizText;
    public TextMeshProUGUI keyTextFromPanel;
    public TextMeshProUGUI ScoreTextFromPanel;
    public TextMeshProUGUI QuizTextFromPanel;
    private int score = 0;
    private int key = 0;
    private int quiz = 0;
    public GameObject PanelMenang;
    public GameObject PanelKalah;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int PointScore)
    {
        score += PointScore;
        UpdateScoreDisplay();
    }

    public void AddKey(int PointKey)
    {
        key += PointKey;
        UpdateKeyDisplay();
    }

    public void AddQuiz(int PointQuiz)
    {
        quiz += PointQuiz;
        UpdateQuizDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    private void UpdateKeyDisplay()
    {
        if (keyText != null)
        {
            keyText.text = key.ToString();
        }
    }

    private void UpdateQuizDisplay()
    {
        if (quizText != null)
        {
            quizText.text = quiz.ToString();
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IsKalah()
    {
        PanelKalah.SetActive(true);
    }

    public void IsMenang()
    {
        PanelMenang.SetActive(true);
        keyTextFromPanel.text = key.ToString();
        ScoreTextFromPanel.text = score.ToString();
        QuizTextFromPanel.text = quiz.ToString();
    }
}