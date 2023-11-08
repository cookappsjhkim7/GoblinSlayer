using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;
    public PoolManager pool;
    public SpawnManager spawn;
    public UI_AtkButtonManager uiAtkbtn;
    public UI_TimeCount uiTimerbar;
    public UI_TextManager uiCombotex;
    public UI_BerserkerGauge uiBerGauge;
    public MoveMap moveMap;
    public HeroController hero;
    public VFXManager vfx;

    public int[] criticalRate;


    public int[] missRate;

    bool isAction;
    WaitForSeconds wfs_01f = new WaitForSeconds(0.1f);

    //public static int[] uiStateRate = new int[4] { 0, 3, 3, 3 };
    //public static int[] uiStateCountRate = new int[3] { 5, 2, 2 };
    //public static int[] unitTypeRate = new int[2] { 8, 2 };


    private void Awake()
    {
        SoundManager.Instance.Play(Enum_Sound.Bgm, "Sound_PlayBGM");
        inst = this;
        isAction = false;

        criticalRate = new int[2] { 80, 20 };
        //missRate = new int[2] { 99, 1 };
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