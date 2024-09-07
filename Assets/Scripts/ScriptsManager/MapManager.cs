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

    private TextMeshProUGUI TextCoinQuest;
    private TextMeshProUGUI TextCoin;
    int gameLvl = 2;
    int coinQuest = 0;
    int coin = 0;
    int key = 0;
    private DataParsistenceManager dataParsistenceManager;

    private void Awake()
    {
        dataParsistenceManager = FindAnyObjectByType<DataParsistenceManager>();
    }

    private void Start()
    {
        DataParsistenceManager.instance.LoadGame();
        // AudioManager.instance.PlayAllAudio();

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
        LoadScene(3);
    }

    private void ChangeSceneGameLvl2()
    {
        if (IsGame2Unlock)
        {
            LoadScene(4);
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
            LoadScene(5);
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
            LoadScene(6);
        }
        else
        {
            gameLvl = 4;
            PanelBukaGame.SetActive(true);
        }
    }

    public void GoToMainMenu()
    {
        LoadScene(0);
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
                    coinQuest -= 4;
                    coin -= 100;
                    // PlayerPrefs.SetInt("Coin_", coin);
                    // PlayerPrefs.SetInt("CoinQuest_", coinQuest);
                    // PlayerPrefs.SetInt("key_", key);
                    DataParsistenceManager.instance.SaveGame();
                    Textcoin.text = coin.ToString();
                    TextcoinQuest.text = coinQuest.ToString();
                    break;

                case 3:
                    IsGame3Unlock = true;
                    PanelBukaGame.SetActive(false);
                    coinQuest -= 4;
                    coin -= 100;
                    // PlayerPrefs.SetInt("Coin_", coin);
                    // PlayerPrefs.SetInt("CoinQuest_", coinQuest);
                    // PlayerPrefs.SetInt("Key_", key);
                    DataParsistenceManager.instance.SaveGame();
                    Textcoin.text = coin.ToString();
                    TextcoinQuest.text = coinQuest.ToString();
                    break;

                case 4:
                    IsGame4Unlock = true;
                    PanelBukaGame.SetActive(false);
                    coinQuest -= 4;
                    coin -= 100;
                    // PlayerPrefs.SetInt("Coin_", coin);
                    // PlayerPrefs.SetInt("CoinQuest_", coinQuest);
                    // PlayerPrefs.SetInt("Key_", key);
                    DataParsistenceManager.instance.SaveGame();
                    Textcoin.text = coin.ToString();
                    TextcoinQuest.text = coinQuest.ToString();
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
        coinQuest = data.DataCoinQuest;
        coin = data.DataCoin;
        key = data.DataKey;
    }

    public void SaveData(GameData data)
    {
        data.DataStatusGame1 = IsGame1Unlock;
        data.DataStatusGame2 = IsGame2Unlock;
        data.DataStatusGame3 = IsGame3Unlock;
        data.DataStatusGame4 = IsGame4Unlock;
        data.DataCoinQuest = coinQuest;
        data.DataCoin = coin;
        data.DataKey = key;
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