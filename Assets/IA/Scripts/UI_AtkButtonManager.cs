using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AtkButtonManager : MonoBehaviour
{
    public HeroController hero;

    public void OnLAtkBtnClick()
    {
        //foreach (MonsterController mon in GameManager.inst.spawn.spawnData)
        //{
        //    for (int i = 0; i < mon.stateBar.curState.Count; i++)
        //    {
        //        mon.stateBar.curState[i].SetActive(false);
        //    }
        //    mon.stateBar.curState.Clear();
        //    mon.stateBar.stateAtk.Clear();
        //    mon.stateBar.stateType.Clear();
        //    mon.gameObject.SetActive(false);
        //}
        //GameManager.inst.spawn.SpawnMonster(0);

        //GameManager.inst.spawn.spawnData[0].Hit();
        //GameManager.inst.uiCombotex.Combo();

        BtnFunction(0);
    }

    public void OnCAtkBtnClick()
    {
        BtnFunction(1);

    }

    public void OnRAtkBtnClick()
    {
        BtnFunction(2);
    }

    void BtnFunction(int heroCurIndex)
    {
        int heroBeforeIndex = hero.existPos;

        hero.Move(heroCurIndex);

        MonsterController mon = GameManager.inst.spawn.spawnData[0];

        if (GameManager.inst.uiBerGauge.isBerserker)
        {
            for (int i = mon.stateCount; i >= 0; i--)
            {
                mon.Hit();
                GameManager.inst.uiCombotex.Combo();
            }
        }
        else
        {
            GameManager.inst.uiTimerbar.TimerReset();

            if (mon.stateBar.stateType[mon.stateCount] == 1)
            {
                if (hero.existPos == mon.existPos)
                {
                    hero.Hit();
                    AttackFail();
                }
                else
                {
                    mon.Move(heroBeforeIndex);

                    if (heroBeforeIndex == heroCurIndex)
                    {
                        hero.Hit();
                        AttackFail();
                    }
                    else
                    {
                        mon.RemoveStateBar();
                        AttackSuccess(GameManager.inst.uiTimerbar.GetTimerSliderValue());
                    }
                }

                //if (hero.existPos == mon.existPos)
                //{
                //    hero.Hit();
                //    AttackFail();
                //}
                //else
                //{
                //    mon.RemoveStateBar();
                //    AttackSuccess(GameManager.inst.uiTimerbar.GetTimerSliderValue());
                //}
            }
            else if (mon.stateBar.stateType[mon.stateCount] == 0)
            {
                if (hero.existPos == mon.existPos)
                {
                    mon.Hit();
                    AttackSuccess(GameManager.inst.uiTimerbar.GetTimerSliderValue());
                }
                else
                {
                    //mon.Move(heroCurIndex);
                    hero.Hit();
                    AttackFail();
                }
            }
            else if (mon.stateBar.stateType[mon.stateCount] == 2)
            {
                mon.Move(mon.existPos - 1);

                //mon.stateBar.stateData[mon.stateCount].stateType = NotNullStateNum(mon);

                int ranNum = Random.Range(0, 4);
                mon.stateBar.SettingState(mon.stateCount, ranNum);

                if (hero.existPos == mon.existPos)
                {
                    mon.Hit();
                    AttackSuccess(GameManager.inst.uiTimerbar.GetTimerSliderValue());
                }
                else
                {
                    hero.Hit();
                    AttackFail();
                }

            }
            else if (mon.stateBar.stateType[mon.stateCount] == 3)
            {
                mon.Move(mon.existPos + 1);

                //mon.stateBar.stateData[mon.stateCount].stateType = NotNullStateNum(mon);
                //mon.stateBar.stateSlot[mon.stateCount].sprite = mon.stateBar.stateData[mon.stateBar.stateType[mon.stateCount]].stateTex;

                int ranNum = Random.Range(0, 4);
                mon.stateBar.SettingState(mon.stateCount, ranNum);

                if (hero.existPos == mon.existPos)
                {
                    mon.Hit();
                    AttackSuccess(GameManager.inst.uiTimerbar.GetTimerSliderValue());
                }
                else
                {
                    hero.Hit();
                    AttackFail();
                }
            }
        }

    }

    //private int NotNullStateNum(MonsterController mon)
    //{
    //    int ranNum;

    //    ranNum = Random.Range(0, 4);

    //    while (true)
    //    {
    //        if (mon.stateBar.stateTex[ranNum] == null)
    //        {
    //            ranNum = Random.Range(0, 4);
    //        }
    //        else
    //        {
    //            break;
    //        }
    //    }

    //    return ranNum;
    //}

    //void RelocationState(MonsterController mon)
    //{
    //    int n;

    //    // 좌우 화살표 검사해서 변경
    //    for (int i = mon.stateCount; i >= 0; i--)
    //    {
    //        if (mon.existPos == 0 && mon.stateBar.stateType[i] == 2)
    //        {
    //            mon.stateBar.stateType[i] = Random.Range(0, 2);
    //            mon.stateBar.stateSlot[i].sprite = mon.stateBar.stateTex[mon.stateBar.stateType[i]];
    //        }
    //        else if (mon.existPos == 2 && mon.stateBar.stateType[i] == 3)
    //        {
    //            mon.stateBar.stateType[i] = Random.Range(0, 2);
    //            mon.stateBar.stateSlot[i].sprite = mon.stateBar.stateTex[mon.stateBar.stateType[i]];
    //        }
    //    }

    //}

    void AttackFail()
    {
        GameManager.inst.uiTimerbar.WaitTimeReset();
        GameManager.inst.uiCombotex.ComboEnd();
        GameManager.inst.uiBerGauge.GaugeDown();
    }

    void AttackSuccess(float gauge)
    {
        GameManager.inst.uiTimerbar.WaitTimeDown();
        GameManager.inst.uiCombotex.Combo();
        GameManager.inst.uiBerGauge.GaugeCharging(1 - gauge);
    }
}
