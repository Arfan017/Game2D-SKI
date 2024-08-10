using UnityEngine;
using System;

public class SaveLoadManager : MonoBehaviour
{
    // Data yang akan disimpan
    public int coin;
    public bool[] materiStatus; // true jika terbuka, false jika terkunci
    public bool[] levelStatus;  // true jika terbuka, false jika terkunci

    // Kunci untuk PlayerPrefs
    private const string COIN_KEY = "Coin";
    private const string MATERI_STATUS_KEY = "MateriStatus";
    private const string LEVEL_STATUS_KEY = "LevelStatus";

    private void Start()
    {
        // Inisialisasi array
        materiStatus = new bool[4]; // Misalnya ada 4 materi
        levelStatus = new bool[10]; // Misalnya ada 10 level
    }

    public void SaveData()
    {
        // Simpan coin
        PlayerPrefs.SetInt(COIN_KEY, coin);

        // Simpan status materi
        string materiStatusString = ConvertBoolArrayToString(materiStatus);
        PlayerPrefs.SetString(MATERI_STATUS_KEY, materiStatusString);

        // Simpan status level
        string levelStatusString = ConvertBoolArrayToString(levelStatus);
        PlayerPrefs.SetString(LEVEL_STATUS_KEY, levelStatusString);

        // Simpan perubahan
        PlayerPrefs.Save();

        Debug.Log("Data berhasil disimpan!");
    }

    public void LoadData()
    {
        // Load coin
        coin = PlayerPrefs.GetInt(COIN_KEY, 0);

        // Load status materi
        string materiStatusString = PlayerPrefs.GetString(MATERI_STATUS_KEY, "");
        materiStatus = ConvertStringToBoolArray(materiStatusString, materiStatus.Length);

        // Load status level
        string levelStatusString = PlayerPrefs.GetString(LEVEL_STATUS_KEY, "");
        levelStatus = ConvertStringToBoolArray(levelStatusString, levelStatus.Length);

        Debug.Log("Data berhasil dimuat!");
    }

    private string ConvertBoolArrayToString(bool[] array)
    {
        string result = "";
        foreach (bool b in array)
        {
            result += b ? "1" : "0";
        }
        return result;
    }

    private bool[] ConvertStringToBoolArray(string s, int length)
    {
        bool[] result = new bool[length];
        for (int i = 0; i < s.Length && i < length; i++)
        {
            result[i] = (s[i] == '1');
        }
        return result;
    }

    // Metode untuk mengatur status materi
    public void SetMateriStatus(int index, bool status)
    {
        if (index >= 0 && index < materiStatus.Length)
        {
            materiStatus[index] = status;
        }
    }

    // Metode untuk mengatur status level
    public void SetLevelStatus(int index, bool status)
    {
        if (index >= 0 && index < levelStatus.Length)
        {
            levelStatus[index] = status;
        }
    }

    // Metode untuk menambah coin
    public void AddCoin(int amount)
    {
        coin += amount;
    }
}