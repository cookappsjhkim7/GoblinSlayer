using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_AtkButtonManager : MonoBehaviour
{
    public HeroController hero;

    public void OnLAtkBtnClick()
    {
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

    public void OnLobbyBtnClick()
    {
        SceneManager.LoadScene("Lobby");
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
                hero.pt.Play();
                GameManager.inst.uiCombotex.Combo();

                hero.Move(mon.existPos);
            }
        }
        else
        {
            GameManager.inst.uiTimerbar.TimerReset();

            if (mon.stateBar.stateType[mon.stateCount] == 0)
            {
                if (hero.existPos == mon.existPos)
                {
                    mon.Hit();
                    hero.pt.Play();
                    AttackSuccess(GameManager.inst.uiTimerbar.GetTimerSliderValue());
                }
                else
                {
                    StartCoroutine(mon.Action_AtkMove(hero.existPos, mon.existPos));
                    hero.Hit();
                    AttackFail();
                }
            }
            else if (mon.stateBar.stateType[mon.stateCount] == 1)
            {
                if (hero.existPos == mon.existPos)
                {
                    StartCoroutine(mon.Action_AtkMove(hero.existPos, mon.existPos));
                    hero.Hit();
                    AttackFail();
                }
                else
                {
                    if (heroBeforeIndex == heroCurIndex)
                    {
                        StartCoroutine(mon.Action_AtkMove(heroBeforeIndex, heroBeforeIndex));
                        hero.Hit();
                        AttackFail();
                    }
                    else
                    {
                        StartCoroutine(mon.Action_AtkMove(heroBeforeIndex, heroBeforeIndex));
                        mon.RemoveStateBar();
                        AttackSuccess(GameManager.inst.uiTimerbar.GetTimerSliderValue());
                    }

                }
            }
            else if (mon.stateBar.stateType[mon.stateCount] == 2)
            {
                mon.Move(mon.existPos - 1);

                if (hero.existPos == mon.existPos)
                {
                    Debug.Log("222");
                    mon.Hit();
                    hero.pt.Play();
                    AttackSuccess(GameManager.inst.uiTimerbar.GetTimerSliderValue());
                }
                else
                {
                    Debug.Log("222222");
                    StartCoroutine(mon.Action_AtkMove(hero.existPos, mon.existPos));
                    hero.Hit();
                    AttackFail();

                    int ranNum = Random.Range(0, 4);
                    mon.stateBar.SettingState(mon.stateCount, ranNum);
                }


            }
            else if (mon.stateBar.stateType[mon.stateCount] == 3)
            {
                mon.Move(mon.existPos + 1);

                if (hero.existPos == mon.existPos)
                {
                    Debug.Log("333");
                    mon.Hit();
                    hero.pt.Play();
                    AttackSuccess(GameManager.inst.uiTimerbar.GetTimerSliderValue());
                }
                else
                {
                    Debug.Log("333333");
                    StartCoroutine(mon.Action_AtkMove(hero.existPos, mon.existPos));
                    //StartCoroutine(mon.Action_MoveAtk(mon.existPos + 1, hero.existPos));
                    hero.Hit();
                    AttackFail();

                    int ranNum = Random.Range(0, 4);
                    mon.stateBar.SettingState(mon.stateCount, ranNum);
                }
            }
        }

    }

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
