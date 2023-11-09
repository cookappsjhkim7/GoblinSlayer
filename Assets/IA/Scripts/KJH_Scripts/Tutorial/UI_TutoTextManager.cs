using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_TutoTextManager : MonoBehaviour
{
    int comboCount = 0;
    public Text comboCountText;

    int killCount = 0;
    public Text killCountText;

    int lvUp = 0;
    public Text lvCountText;

    int coinCount = 0;
    public Text coinCountText;

    public int[,] spawnRate = new int[4, 6]
    {
        { 1, 0, 0, 0 ,0 ,0},
        { 0, 1, 0, 0 ,0 ,0},
        { 0, 0, 1, 0 ,0 ,0},
        { 0, 0, 0, 1 ,1 ,1}
    };
    int[] curSpawnRate = new int[7];

    public void KillCount()
    {
        killCount++;
        killCountText.text = killCount.ToString();
        
        GameManager.Instance.spawn.SpawnNextMonster();
        
        //.. 200부터 소환 안되게
    }


    public void CoinCount()
    {
        coinCount++;
        coinCountText.text = coinCount.ToString();
    }


    public void lvCount()
    {
        lvUp++;
        //lvCountText.text = lvUp.ToString();

        if (lvUp == 0)
        {
            ChangeSpawnRate(0);
            // 설명 텍스쳐 나오면 될듯
            // 화면 멈추고
        }
        else if (lvUp == 20)
        {
            ChangeSpawnRate(1);
            // 설명 텍스쳐 나오면 될듯
            // 화면 멈추고
        }
        else if (lvUp == 40)
        {
            ChangeSpawnRate(2);
            // 설명 텍스쳐 나오면 될듯
            // 화면 멈추고
        }
        else if (lvUp == 60)
        {
            ChangeSpawnRate(3);
        }        
    }

    public void ChangeSpawnRate(int n)
    {
        for (int i = 0; i < 6; i++)
        {
            curSpawnRate[i] = spawnRate[n, i];
        }
    }

    public void Combo()
    {
        comboCount++;

        comboCountText.transform.DOKill();
        
        if(comboCount%10 == 0)
        {
            comboCountText.transform.localScale = Vector3.one * 3f;
            comboCountText.transform.DOScale(1, 0.2f);
        }
        else
        {
            comboCountText.transform.localScale = Vector3.one * 2f;
            comboCountText.transform.DOScale(1, 0.2f);
        }

        comboCountText.text = comboCount.ToString();
    }

    public void ComboEnd()
    {
        comboCount = 0;
        comboCountText.text = comboCount.ToString();
    }

    public void KillReset()
    {
        killCount = 0;
        killCountText.text = killCount.ToString();
    }
}
