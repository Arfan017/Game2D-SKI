using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
