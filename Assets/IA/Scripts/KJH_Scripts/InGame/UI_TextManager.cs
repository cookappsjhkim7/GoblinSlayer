using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_TextManager : MonoBehaviour
{
    int comboCount = 0;
    public Text comboCountText;

    int killCount = 0;
    public Text killCountText;

    int lvUp = 0;
    public Text lvCountText;

    int coinCount = 0;
    public Text coinCountText;

    public void KillCount()
    {
        killCount++;
        killCountText.text = killCount.ToString();
        
        GameManager.inst.spawn.SpawnNextMonster();
        
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
            GameManager.inst.spawn.ChangeSpawnRate(0);
        }
        else if (lvUp == 15)
        {
            GameManager.inst.spawn.ChangeSpawnRate(1);
        }
        else if (lvUp == 30)
        {
            GameManager.inst.spawn.ChangeSpawnRate(2);
        }
        else if (lvUp == 45)
        {
            GameManager.inst.spawn.ChangeSpawnRate(3);
        }
        else if (lvUp == 60)
        {
            GameManager.inst.spawn.ChangeSpawnRate(4);
        }
        else if (lvUp == 75)
        {
            GameManager.inst.spawn.ChangeSpawnRate(5);
        }
        else if (lvUp == 90)
        {
            GameManager.inst.spawn.ChangeSpawnRate(6);
        }
        else if (lvUp == 105)
        {
            GameManager.inst.spawn.ChangeSpawnRate(7);
        }
        else if (lvUp == 120)
        {
            GameManager.inst.spawn.ChangeSpawnRate(8);
        }
        else if (lvUp == 135)
        {
            GameManager.inst.spawn.ChangeSpawnRate(9);
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
