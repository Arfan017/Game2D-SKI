using UnityEngine;
using UnityEngine.UI;

public class SurpriseItem : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float maxHeight = 2f;
    public bool isQuestion = false;
    public string question = "Apa ibukota Indonesia?";
    public string[] answers = { "Jakarta", "Surabaya", "Bandung", "Medan" };
    public int correctAnswerIndex = 0;

    private Vector3 startPosition;
    private GameObject questionPanel;

    void Start()
    {
        startPosition = transform.position;
        if (isQuestion)
        {
            CreateQuestionPanel();
        }
        else
        {
            // Jika ini coin, gerakkan ke atas
            StartCoroutine(MoveUp());
        }
    }

    System.Collections.IEnumerator MoveUp()
    {
        while (transform.position.y < startPosition.y + maxHeight)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            yield return null;
        }

        if (!isQuestion)
        {
            // Jika ini coin, hancurkan setelah mencapai ketinggian maksimum
            Destroy(gameObject);
        }
    }

    void CreateQuestionPanel()
    {
        // questionPanel = new GameObject("QuestionPanel");
        // questionPanel.AddComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        // questionPanel.AddComponent<CanvasScaler>();
        // questionPanel.AddComponent<GraphicRaycaster>();

        // RectTransform rectTransform = questionPanel.GetComponent<RectTransform>();
        // rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300);
        // rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200);
        // rectTransform.position = transform.position + Vector3.up * 2;

        // GameObject textObj = new GameObject("QuestionText");
        // textObj.transform.SetParent(questionPanel.transform, false);
        // Text questionText = textObj.AddComponent<Text>();
        // questionText.text = question;
        // questionText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        // questionText.alignment = TextAnchor.MiddleCenter;

        // for (int i = 0; i < answers.Length; i++)
        // {
        //     CreateAnswerButton(answers[i], i);
        // }
    }

    void CreateAnswerButton(string answerText, int index)
    {
        // GameObject buttonObj = new GameObject("AnswerButton" + index);
        // buttonObj.transform.SetParent(questionPanel.transform, false);

        // Button button = buttonObj.AddComponent<Button>();
        // Text buttonText = buttonObj.AddComponent<Text>();
        // buttonText.text = answerText;
        // buttonText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        // buttonText.alignment = TextAnchor.MiddleCenter;

        // RectTransform rectTransform = buttonObj.GetComponent<RectTransform>();
        // rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
        // rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 30);
        // rectTransform.anchoredPosition = new Vector2(0, -50 - (40 * index));

        // button.onClick.AddListener(() => OnAnswerSelected(index));
    }

    void OnAnswerSelected(int index)
    {
        if (index == correctAnswerIndex)
        {
            Debug.Log("Jawaban Benar!");
        }
        else
        {
            Debug.Log("Jawaban Salah!");
        }
        Destroy(questionPanel);
        Destroy(gameObject);
    }
}