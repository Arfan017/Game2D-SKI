using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    void Start()
    {
        AudioManager.instance.PlayAllAudio();
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

    public void LoadScene(int indexScene)
    {
        // loadingScreen.SetActive(true);
        StartCoroutine(LoadSceneAsynchronously(indexScene));
    }

    IEnumerator LoadSceneAsynchronously(int indexScene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(indexScene);

        // operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // float progress = Mathf.Clamp01(operation.progress / 0.9f);
            // progressBar.value = progress;

            // if (operation.progress >= 0.9f)
            // {
            //     progressBar.value = 1f;
            //     operation.allowSceneActivation = true;
            // }

            yield return null;
        }
    }
}
