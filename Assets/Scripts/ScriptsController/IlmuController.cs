using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class IlmuController : MonoBehaviour
{
    public TextMeshProUGUI TitleText;
    public String Title;
    public TextMeshProUGUI DescriptionText;
    [TextArea(10, 10)]
    public String Description;

    void Start()
    {
        PauseGame();

        TitleText.text = Title;
        DescriptionText.text = Description;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
