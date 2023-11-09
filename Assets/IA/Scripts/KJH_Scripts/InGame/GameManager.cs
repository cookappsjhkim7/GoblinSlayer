using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public PoolManager pool;
    public SpawnManager spawn;
    public UI_AtkButtonManager uiAtkbtn;
    public UI_TimeCount uiTimerbar;
    public UI_TextManager uiCombotex;
    public UI_BerserkerGauge uiBerGauge;
    //public MoveMap moveMap;
    public HeroController hero;
    public VFXManager vfx;

    bool isAction;
    WaitForSeconds wfs_01f = new WaitForSeconds(0.1f);

    private void Awake()
    {
        isAction = false;
    }

    private void Start()
    {
        SoundManager.Instance.Play(Enum_Sound.Bgm, "Sound_PlayBGM");
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
}
