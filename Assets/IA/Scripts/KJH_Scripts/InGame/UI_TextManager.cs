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
    
    public Text coinCountText;

    public void KillCount()
    {
        killCount++;
        killCountText.text = killCount.ToString();
        
        GameManager.Instance.spawn.SpawnNextMonster();
        LobbyManager.Instance.SetMaxScore(killCount);
    }

    public void CoinCount()
    {
        LobbyManager.Instance.SaveData.currency++;
        coinCountText.text = LobbyManager.Instance.SaveData.currency.ToString();
    }

    public void lvCount()
    {
        lvUp++;
        //lvCountText.text = lvUp.ToString();

        if (lvUp == 0)
        {
            GameManager.Instance.spawn.ChangeSpawnRate(0);
        }
        else if (lvUp == 15)
        {
            GameManager.Instance.spawn.ChangeSpawnRate(1);
        }
        else if (lvUp == 30)
        {
            GameManager.Instance.spawn.ChangeSpawnRate(2);
        }
        else if (lvUp == 45)
        {
            GameManager.Instance.spawn.ChangeSpawnRate(3);
        }
        else if (lvUp == 60)
        {
            GameManager.Instance.spawn.ChangeSpawnRate(4);
        }
        else if (lvUp == 75)
        {
            GameManager.Instance.spawn.ChangeSpawnRate(5);
        }
        else if (lvUp == 90)
        {
            GameManager.Instance.spawn.ChangeSpawnRate(6);
        }
        else if (lvUp == 105)
        {
            GameManager.Instance.spawn.ChangeSpawnRate(7);
        }
        else if (lvUp == 120)
        {
            GameManager.Instance.spawn.ChangeSpawnRate(8);
        }
        else if (lvUp == 135)
        {
            GameManager.Instance.spawn.ChangeSpawnRate(9);
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
