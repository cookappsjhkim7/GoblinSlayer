using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TextManager : MonoBehaviour
{
    int comboCount = 0;
    public Text comboCountText;
    
    int killCount = 0;
    public Text killCountText;

    int lvUp = 0;
    public Text lvCountText;

    public void KillCount()
    {
        killCount++;
        killCountText.text = killCount.ToString();


        if (lvUp == 0)
        {
            GameManager.inst.spawn.ChangeSpawnRate(0);
        }
        else if (lvUp == 10)
        {
            GameManager.inst.spawn.ChangeSpawnRate(1);
        }
        else if (lvUp == 20)
        {
            GameManager.inst.spawn.ChangeSpawnRate(2);
        }
        else if (lvUp == 30)
        {
            GameManager.inst.spawn.ChangeSpawnRate(3);
        }
        else if (lvUp == 40)
        {
            GameManager.inst.spawn.ChangeSpawnRate(4);
        }
        else if (lvUp == 50)
        {
            GameManager.inst.spawn.ChangeSpawnRate(5);
        }
        else if (lvUp == 60)
        {
            GameManager.inst.spawn.ChangeSpawnRate(6);
        }
        else if (lvUp == 70)
        {
            GameManager.inst.spawn.ChangeSpawnRate(7);
        }
        else if (lvUp == 80)
        {
            GameManager.inst.spawn.ChangeSpawnRate(8);
        }
        else if (lvUp == 90)
        {
            GameManager.inst.spawn.ChangeSpawnRate(9);
        }
        else if (lvUp == 100)
        {
            GameManager.inst.spawn.ChangeSpawnRate(10);
        }


    }

    public void lvCount()
    {
        lvUp++;
        lvCountText.text = lvUp.ToString();

        if (lvUp == 0)
        {
            GameManager.inst.spawn.ChangeSpawnRate(0);
        }
        else if (lvUp == 10)
        {
            GameManager.inst.spawn.ChangeSpawnRate(1);
        }
        else if (lvUp == 20)
        {
            GameManager.inst.spawn.ChangeSpawnRate(2);
        }
        else if (lvUp == 30)
        {
            GameManager.inst.spawn.ChangeSpawnRate(3);
        }
        else if (lvUp == 40)
        {
            GameManager.inst.spawn.ChangeSpawnRate(4);
        }
        else if (lvUp == 50)
        {
            GameManager.inst.spawn.ChangeSpawnRate(5);
        }
        else if (lvUp == 60)
        {
            GameManager.inst.spawn.ChangeSpawnRate(6);
        }
        else if (lvUp == 70)
        {
            GameManager.inst.spawn.ChangeSpawnRate(7);
        }
        else if (lvUp == 80)
        {
            GameManager.inst.spawn.ChangeSpawnRate(8);
        }
        else if (lvUp == 90)
        {
            GameManager.inst.spawn.ChangeSpawnRate(9);
        }
        else if (lvUp == 100)
        {
            GameManager.inst.spawn.ChangeSpawnRate(10);
        }


    }

    public void Combo()
    {
        comboCount++;
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
