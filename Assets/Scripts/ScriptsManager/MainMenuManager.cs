using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    public void MainGame()
    {
        SceneManager.LoadScene("MapScene");
    }

    public void Belajar()
    {
        SceneManager.LoadScene("BelajarScene");
    }

    public void KeluarGame()
    {
        Application.Quit();
    }
}
