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

    //public static int[] uiStateRate = new int[4] { 0, 3, 3, 3 };
    //public static int[] uiStateCountRate = new int[3] { 5, 2, 2 };
    //public static int[] unitTypeRate = new int[2] { 8, 2 };


    private void Awake()
    {
        inst = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            uiAtkbtn.OnLAtkBtnClick();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            uiAtkbtn.OnCAtkBtnClick();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            uiAtkbtn.OnRAtkBtnClick();
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
}
