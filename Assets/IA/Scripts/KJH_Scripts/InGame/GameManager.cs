using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;
    public static SaveData SaveData;
    public PoolManager pool;
    public SpawnManager spawn;
    public UI_AtkButtonManager uiAtkbtn;
    public UI_TimeCount uiTimerbar;
    public UI_TextManager uiCombotex;
    public UI_BerserkerGauge uiBerGauge;
    //public MoveMap moveMap;
    public HeroController hero;
    public VFXManager vfx;

    public int[] missRate;

    bool isAction;
    WaitForSeconds wfs_01f = new WaitForSeconds(0.1f);

    private void Awake()
    {
        SoundManager.Instance.Play(Enum_Sound.Bgm, "Sound_PlayBGM");
        inst = this;
        isAction = false;
        
        SaveData ??= new SaveData();
        SaveData.LoadGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if( isAction == false)
            {
                isAction = true;
                uiAtkbtn.OnLAtkBtnClick();
                StartCoroutine(CoIsAction());
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (isAction == false)
            {
                isAction = true;
                uiAtkbtn.OnCAtkBtnClick();
                StartCoroutine(CoIsAction());
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (isAction == false)
            {
                isAction = true;
                uiAtkbtn.OnRAtkBtnClick();
                StartCoroutine(CoIsAction());
            }
        }
    }

    public static int RandomRate(int startNum, int[] rate)
    {
        int sumTotal = 0;
        int sumPart = rate[0];

        foreach (int item in rate)
        {
            sumTotal += item;
        }

        int num = Random.Range(0, sumTotal);

        if (num < rate[0])
        {
            return 0 + startNum;
        }
        else
        {
            for (int i = 1; i < rate.Length; i++)
            {
                sumPart += rate[i];

                if (rate[i - 1] <= num && num < sumPart)
                {
                    return i + startNum;
                }
            }
        }
        return -100;
    }

    IEnumerator CoIsAction()
    {
        yield return wfs_01f;

        isAction = false;
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

            var weaponList = WeaponData.Instance.weaponList;

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

        var weaponData =  WeaponData.Instance.weaponList;
        var saveInt = 0;

        int value = 1;
        for (int i = 0 ; i < weaponData.Count ; i++)
        {
            if (weaponData[i].isBbuy)
            {
                saveInt += (int)Math.Pow(value,2);
            }

            value++;
        }
        
        PlayerPrefs.SetInt(CHARACTER, saveInt);
    }
}