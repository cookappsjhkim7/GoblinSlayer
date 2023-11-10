using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : SingletonMonoBehaviour<LobbyManager>
{
    public WeaponData weaponData;
    public Stat stat;

    public SaveData SaveData;

    public bool CowOn = false;

    public void Awake()
    {
        SaveData ??= new SaveData();
        LoadGame();
    }
    
    public void SetMaxScore(int value)
    {
        SaveData.maxScore = Mathf.Max(SaveData.maxScore, value);
    }

    public void SaveGame()
    {
        SaveData.Save();
    }

    public void LoadGame()
    {
        SaveData.LoadGame();
    }
}