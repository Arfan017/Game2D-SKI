using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public void ChangeSceneGameLvl1()
    {
        SceneManager.LoadScene("gamelvl1");
    }

    public void ChangeSceneGameLvl2()
    {
        SceneManager.LoadScene("gamelvl2");
    }

    public void ChangeSceneGameLvl3()
    {
        SceneManager.LoadScene("gamelvl3");
    }

    public void ChangeSceneGameLvl4()
    {
        SceneManager.LoadScene("gamelvl4");
    }
}
