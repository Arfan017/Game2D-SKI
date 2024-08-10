using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour, IDataPersistence
{

    public Button[] buttonGameLvl;
    public GameObject PanelBukaGame;
    public GameObject PanelPeringatan;
    public TextMeshProUGUI Textcoin;
    public TextMeshProUGUI TextcoinQuest;
    Boolean isGame1Unlock;
    Boolean isGame2Unlock = false;
    Boolean isGame3Unlock = false;
    Boolean isGame4Unlock = false;

    public bool IsGame1Unlock { get => isGame1Unlock; set => isGame1Unlock = value; }
    public bool IsGame2Unlock { get => isGame2Unlock; set => isGame2Unlock = value; }
    public bool IsGame3Unlock { get => isGame3Unlock; set => isGame3Unlock = value; }
    public bool IsGame4Unlock { get => isGame4Unlock; set => isGame4Unlock = value; }
    int gameLvl = 2;
    int coinQuest = 5;
    int coin = 100;

    private DataParsistenceManager dataParsistenceManager;

    private void Awake()
    {
        dataParsistenceManager = FindAnyObjectByType<DataParsistenceManager>();

    }

    private void Start()
    {
        DataParsistenceManager.instance.LoadGame();

        Textcoin.text = coin.ToString();
        TextcoinQuest.text = coinQuest.ToString();

        if (buttonGameLvl != null && buttonGameLvl.Length >= 4)
        {
            if (buttonGameLvl[0] != null) buttonGameLvl[0].onClick.AddListener(ChangeSceneGameLvl1);
            if (buttonGameLvl[1] != null) buttonGameLvl[1].onClick.AddListener(ChangeSceneGameLvl2);
            if (buttonGameLvl[2] != null) buttonGameLvl[2].onClick.AddListener(ChangeSceneGameLvl3);
            if (buttonGameLvl[3] != null) buttonGameLvl[3].onClick.AddListener(ChangeSceneGameLvl4);
        }
        else
        {
            Debug.LogWarning("Tidak cukup tombol game yang ditetapkan.");
        }
    }

    public void ChangeSceneGameLvl1()
    {
        SceneManager.LoadScene("gamelvl1");
    }

    private void ChangeSceneGameLvl2()
    {
        if (IsGame2Unlock)
        {
            SceneManager.LoadScene("gamelvl2");
        }
        else
        {
            gameLvl = 2;
            PanelBukaGame.SetActive(true);
        }
    }

    public void ChangeSceneGameLvl3()
    {
        if (IsGame3Unlock)
        {
            SceneManager.LoadScene("gamelvl3");
        }
        else
        {
            gameLvl = 3;
            PanelBukaGame.SetActive(true);
        }
    }

    public void ChangeSceneGameLvl4()
    {
        if (IsGame4Unlock)
        {
            SceneManager.LoadScene("gamelvl4");
        }
        else
        {
            gameLvl = 4;
            PanelBukaGame.SetActive(true);
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1;
    }

    public void BukaGame()
    {
        if (coinQuest >= 4 && coin >= 100)
        {
            switch (gameLvl)
            {
                case 2:
                    IsGame2Unlock = true;
                    PanelBukaGame.SetActive(false);
                    dataParsistenceManager.SaveGame();
                    coinQuest -= 4;
                    coin -= 100;
                    break;

                case 3:
                    IsGame3Unlock = true;
                    PanelBukaGame.SetActive(false);
                    dataParsistenceManager.SaveGame();
                    coinQuest -= 4;
                    coin -= 100;
                    break;

                case 4:
                    IsGame4Unlock = true;
                    PanelBukaGame.SetActive(false);
                    dataParsistenceManager.SaveGame();
                    coinQuest -= 4;
                    coin -= 100;
                    break;
            }
        }
        else
        {
            PanelPeringatan.SetActive(true);
        }
    }

    public void LoadData(GameData data)
    {
        IsGame1Unlock = data.DataStatusGame1;
        IsGame2Unlock = data.DataStatusGame2;
        IsGame3Unlock = data.DataStatusGame3;
        IsGame4Unlock = data.DataStatusGame4;
    }

    public void SaveData(GameData data)
    {
        data.DataStatusGame1 = IsGame1Unlock;
        data.DataStatusGame2 = IsGame2Unlock;
        data.DataStatusGame3 = IsGame3Unlock;
        data.DataStatusGame4 = IsGame4Unlock;
    }
}