using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int DataCoin;
    public int DataCoinQuest;
    public int DataKey;
    public Boolean DataStatusGame1;
    public Boolean DataStatusGame2;
    public Boolean DataStatusGame3;
    public Boolean DataStatusGame4;
    public Boolean DataStatusMateri1;
    public Boolean DataStatusMateri2;
    public Boolean DataStatusMateri3;
    public Boolean DataStatusMateri4;

    public GameData()
    {
        this.DataCoin = 0;
        this.DataCoinQuest = 0;
        this.DataKey = 0;
        this.DataStatusGame1 = false;
        this.DataStatusGame2 = false;
        this.DataStatusGame3 = false;
        this.DataStatusGame4 = false;
        this.DataStatusMateri1 = false;
        this.DataStatusMateri2 = false;
        this.DataStatusMateri3 = false;
        this.DataStatusMateri4 = false;
    }
}