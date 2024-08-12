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
    public GameObject PanelMenang;
    public GameObject PanelKalah;
    public GameObject PanelPause;
    public AudioSource audioCollectCoin;
    public AudioSource audioCollectKey;
    public AudioSource audiokalah;
    public AudioSource audioMenang;
    private int score = 0;
    private int key = 0;
    private int quiz = 0;
    private DataParsistenceManager dataParsistenceManager;
    private AudioManager audioManager;

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
        audioManager = FindAnyObjectByType<AudioManager>();
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
            audioCollectCoin.Play();
        }
    }

    private void UpdateKeyDisplay()
    {
        if (keyText != null)
        {
            keyText.text = key.ToString();
            audioCollectKey.Play();
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
        // if (audioManager != null)
        // {
        //     audioManager.StopAllAudio(); // Metode yang Anda buat di AudioManager
        // }
        Destroy(audioManager.gameObject);
        AudioManager.instance.StopAllAudio();
        audiokalah.Play();
        PanelKalah.SetActive(true);
    }

    public void Selanjutnya()
    {
        // int Coin_ = PlayerPrefs.GetInt("Coin_", 0);
        // int CoinQuest_ = PlayerPrefs.GetInt("CoinQuest_", 0);
        // int Key_ = PlayerPrefs.GetInt("Key_", 0);

        // score += Coin_;
        // quiz += CoinQuest_;
        // key += Key_;

        // dataParsistenceManager.SaveGame();
        // audioManager.PlayAllAudio();
        // Destroy(audioManager.gameObject);
        DataParsistenceManager.instance.SaveGame();
        SceneManager.LoadScene("MapScene");
        Time.timeScale = 1;
    }

    public void IsMenang()
    {
        // if (audioManager != null)
        // {
        //     audioManager.StopAllAudio(); // Metode yang Anda buat di AudioManager
        // }

        Destroy(audioManager.gameObject);
        audioMenang.Play();
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
        // Destroy(audioManager.gameObject);
        DataParsistenceManager.instance.SetSaveOnUnload(false);
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1;
    }

    public void SaveData(GameData data)
    {
        data.DataCoin = score;
        data.DataKey = key;
        data.DataCoinQuest = quiz;
    }
    public void LoadData(GameData data)
    {
        // throw new System.NotImplementedException();
    }
}