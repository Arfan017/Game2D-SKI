using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IDataPersistence
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
    public GameObject PanelPause;
    private DataParsistenceManager dataParsistenceManager;

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


    private void Start()
    {
        dataParsistenceManager = FindAnyObjectByType<DataParsistenceManager>();
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

    public void Selanjutnya()
    {
        dataParsistenceManager.SaveGame();
        SceneManager.LoadScene("MapScene");
        Time.timeScale = 1;
    }

    public void IsMenang()
    {
        PanelMenang.SetActive(true);
        keyTextFromPanel.text = key.ToString();
        ScoreTextFromPanel.text = score.ToString();
        QuizTextFromPanel.text = quiz.ToString();
    }

    public void Pause()
    {
        PanelPause.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        PanelPause.SetActive(false);
        Time.timeScale = 1;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1;
    }

    public void LoadData(GameData data)
    {

    }

    public void SaveData(GameData data)
    {
        data.DataCoin = score;
        data.DataKey = key;
        data.DataCoinQuest = quiz;
    }
}