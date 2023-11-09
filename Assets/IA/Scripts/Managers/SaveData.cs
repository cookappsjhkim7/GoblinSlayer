using System;
using UnityEngine;

public class SaveData
{
    public int maxScore;
    public int currency;
    public int character;

    public static readonly string MAXSCORE = "MAXSCORE";
    public static readonly string CURRENCY = "CURRENCY";
    public static readonly string CHARACTER = "CHARACTER";

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey(MAXSCORE))
        {
            maxScore = PlayerPrefs.GetInt("MAXSCORE");
        }

        if (PlayerPrefs.HasKey(CURRENCY))
        {
            currency = PlayerPrefs.GetInt("CURRENCY");
        }

        if (PlayerPrefs.HasKey(CHARACTER))
        {
            character = PlayerPrefs.GetInt(CHARACTER);

            var convertString = Convert.ToString(character, 2);

            var weaponList = LobbyManager.Instance.weaponData.weaponList;

            for (int i = 0; i < convertString.Length; i++)
            {
                if (convertString[i] == '1')
                {
                    weaponList[i].isBbuy = true;
                }
                else
                {
                    weaponList[i].isBbuy = false;
                }
            }
        }

        Save();
    }

    public void Save()
    {
        PlayerPrefs.SetInt(MAXSCORE, maxScore);
        PlayerPrefs.SetInt(CURRENCY, currency);

        var weaponData = LobbyManager.Instance.weaponData.weaponList;
        var saveInt = 0;

        int value = 1;
        for (int i = 0; i < weaponData.Count; i++)
        {
            if (weaponData[i].isBbuy)
            {
                saveInt += (int)Math.Pow(value, 2);
            }

            value++;
        }

        PlayerPrefs.SetInt(CHARACTER, saveInt);
    }
}