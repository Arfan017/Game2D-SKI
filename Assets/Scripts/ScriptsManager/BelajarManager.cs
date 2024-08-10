using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BelajarManager : MonoBehaviour, IDataPersistence
{
    public UnityEngine.UI.Button[] buttonMateri;
    public Sprite imageMateri1Unlock;
    public Sprite imageMateri2Unlock;
    public Sprite imageMateri3Unlock;
    public Sprite imageMateri4Unlock;
    public GameObject PanelMateri1;
    public GameObject PanelMateri2;
    public GameObject PanelMateri3;
    public GameObject PanelMateri4;
    public GameObject PanelBukaMateri;
    public GameObject PanelPeringatan;
    Boolean isMateri1Unlock;
    Boolean isMateri2Unlock;
    Boolean isMateri3Unlock;
    Boolean isMateri4Unlock;
    int MateriSaatIni = 0;
    int key = 12;
    private DataParsistenceManager dataParsistenceManager;

    public bool IsMateri1Unlock { get => isMateri1Unlock; set => isMateri1Unlock = value; }
    public bool IsMateri2Unlock { get => isMateri2Unlock; set => isMateri2Unlock = value; }
    public bool IsMateri3Unlock { get => isMateri3Unlock; set => isMateri3Unlock = value; }
    public bool IsMateri4Unlock { get => isMateri4Unlock; set => isMateri4Unlock = value; }

    private void Awake()
    {
        dataParsistenceManager = FindAnyObjectByType<DataParsistenceManager>();
    }


    private void Start()
    {

        DataParsistenceManager.instance.LoadGame();

        if (buttonMateri != null && buttonMateri.Length >= 4)
        {
            if (buttonMateri[0] != null) buttonMateri[0].onClick.AddListener(Materi1);
            if (buttonMateri[1] != null) buttonMateri[1].onClick.AddListener(Materi2);
            if (buttonMateri[2] != null) buttonMateri[2].onClick.AddListener(Materi3);
            if (buttonMateri[3] != null) buttonMateri[3].onClick.AddListener(Materi4);
        }
        else
        {
            Debug.LogWarning("Tidak cukup tombol materi yang ditetapkan.");
        }

        dataParsistenceManager.LoadGame();

        if (IsMateri1Unlock)
        {
            buttonMateri[0].GetComponent<UnityEngine.UI.Image>().sprite = imageMateri1Unlock;
        }

        if (IsMateri2Unlock)
        {
            buttonMateri[1].GetComponent<UnityEngine.UI.Image>().sprite = imageMateri2Unlock;
        }

        if (IsMateri3Unlock)
        {
            buttonMateri[2].GetComponent<UnityEngine.UI.Image>().sprite = imageMateri3Unlock;
        }

        if (IsMateri4Unlock)
        {
            buttonMateri[3].GetComponent<UnityEngine.UI.Image>().sprite = imageMateri4Unlock;
        }

    }

    private void Materi1()
    {
        if (IsMateri1Unlock)
        {
            PanelMateri1.SetActive(true);
        }
        else
        {
            MateriSaatIni = 1;
            PanelBukaMateri.SetActive(true);
        }
    }

    private void Materi2()
    {
        if (IsMateri2Unlock)
        {
            PanelMateri2.SetActive(true);
        }
        else
        {
            MateriSaatIni = 2;
            PanelBukaMateri.SetActive(true);
        }
    }

    private void Materi3()
    {
        if (IsMateri3Unlock)
        {
            PanelMateri3.SetActive(true);
        }
        else
        {
            MateriSaatIni = 3;
            PanelBukaMateri.SetActive(true);
        }
    }

    private void Materi4()
    {
        if (IsMateri4Unlock)
        {
            PanelMateri4.SetActive(true);
        }
        else
        {
            MateriSaatIni = 4;
            PanelBukaMateri.SetActive(true);
        }
    }

    public void BukaMater()
    {
        if (key >= 3)
        {
            switch (MateriSaatIni)
            {
                case 1:
                    IsMateri1Unlock = true;
                    PanelBukaMateri.SetActive(false);
                    dataParsistenceManager.SaveGame();
                    buttonMateri[0].GetComponent<UnityEngine.UI.Image>().sprite = imageMateri1Unlock;
                    key -= 3;
                    break;
                case 2:
                    IsMateri2Unlock = true;
                    PanelBukaMateri.SetActive(false);
                    dataParsistenceManager.SaveGame();
                    buttonMateri[1].GetComponent<UnityEngine.UI.Image>().sprite = imageMateri2Unlock;
                    key -= 3;
                    break;
                case 3:
                    IsMateri3Unlock = true;
                    PanelBukaMateri.SetActive(false);
                    dataParsistenceManager.SaveGame();
                    buttonMateri[2].GetComponent<UnityEngine.UI.Image>().sprite = imageMateri3Unlock;
                    key -= 3;
                    break;
                case 4:
                    IsMateri4Unlock = true;
                    PanelBukaMateri.SetActive(false);
                    dataParsistenceManager.SaveGame();
                    buttonMateri[3].GetComponent<UnityEngine.UI.Image>().sprite = imageMateri4Unlock;
                    key -= 3;
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
        IsMateri1Unlock = data.DataStatusMateri1;
        IsMateri2Unlock = data.DataStatusMateri2;
        IsMateri3Unlock = data.DataStatusMateri3;
        IsMateri4Unlock = data.DataStatusMateri4;
    }

    public void SaveData(GameData data)
    {
        data.DataStatusMateri1 = IsMateri1Unlock;
        data.DataStatusMateri2 = IsMateri2Unlock;
        data.DataStatusMateri3 = IsMateri3Unlock;
        data.DataStatusMateri4 = IsMateri4Unlock;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1;
    }
}