using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class QuizQuestion
    {
        public string question;
        public string[] answers;
        public int correctAnswerIndex;
    }
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public TextMeshProUGUI scoreText;
    public GameObject quizPanel;
    public List<QuizQuestion> questions = new List<QuizQuestion>();
    private int currentQuestionIndex = 0;
    private int score = 0;

    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        PauseGame();
        if (questions.Count > 0)
        {
            DisplayQuestion(currentQuestionIndex);
        }
        else
        {
            Debug.Log("No questions added to the quiz!");
        }

        // UpdateScoreText();
    }

    void DisplayQuestion(int index)
    {
        if (index < questions.Count)
        {
            QuizQuestion question = questions[index];
            questionText.text = question.question;

            for (int i = 0; i < answerButtons.Length; i++)
            {
                int answerIndex = i; // Capture the index for the lambda
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = question.answers[i];
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => CheckAnswer(answerIndex));
            }
        }
        else
        {
            EndQuiz();
        }
    }

    void CheckAnswer(int selectedAnswerIndex)
    {
        QuizQuestion currentQuestion = questions[currentQuestionIndex];

        if (selectedAnswerIndex == currentQuestion.correctAnswerIndex)
        {
            // score++;
            // UpdateScoreText();
            Debug.Log("Jawaban Benar");
            ResumeGame();
            gameObject.SetActive(false);
            gameManager.AddQuiz(1);
        }
        else
        {
            Debug.Log("Jawaban Salah");
            ResumeGame();
            gameObject.SetActive(false);

        }

        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Count)
        {
            DisplayQuestion(currentQuestionIndex);
        }
        else
        {
            EndQuiz();
        }
    }

    void EndQuiz()
    {
        // questionText.text = "Quiz Completed!";
        foreach (Button button in answerButtons)
        {
            // button.gameObject.SetActive(false);
            button.interactable = false;
        }
        // Debug.Log("Final Score: " + score + "/" + questions.Count);
        Debug.Log("Quiz Selesai");
        ResumeGame();
        gameObject.SetActive(false);

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}