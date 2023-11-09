using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_AtkButtonManager : MonoBehaviour
{
    public HeroController hero;
    float time;

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

    public void CriticalTimeScaleCon()
    {
        Time.timeScale = 1;
        StopCoroutine(CoCritcalTime());
        StartCoroutine(CoCritcalTime());
    }

    IEnumerator CoCritcalTime()
    {
        //Time.timeScale = 0.2f;

        //yield return new WaitForSecondsRealtime(0.2f);

        //Time.timeScale = 1;

        //yield break;

        while (true)
        {
            time += 0.1f;

            Time.timeScale = time;

            yield return new WaitForSecondsRealtime(0.1f);

            if (time >= 1)
            {
                time = 0;
                Time.timeScale = 1;
                yield break;
            }
        }
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
                SoundManager.Instance.PlayRandomAttack(0.4f,1f);
                mon.Hit(false);
                //hero.BerserkerAttack();
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
                    if (GameManager.RandomRate(0, hero.criticalRate) == 1)
                    {
                        mon.criticalHit = true;
                        mon.Hit(false);

                        CriticalTimeScaleCon();
                        GameManager.inst.vfx.SpawnVFX(3, mon.transform.position);
                        SoundManager.Instance.Play(Enum_Sound.Effect, "Sound_CriticalHit");
                    }
                    else
                    {
                        SoundManager.Instance.PlayRandomAttack();
                        mon.Hit(false);
                    }
                    hero.Attack();
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
                //mon.Move(mon.existPos - 1);

                int monNextMove = (mon.existPos - 1 == -1) ? 2 : mon.existPos - 1;

                if (hero.existPos == monNextMove)
                {
                    mon.Move(mon.existPos - 1);
                    
                    if (GameManager.RandomRate(0, hero.criticalRate) == 1)
                    {
                        mon.criticalHit = true;
                        mon.Hit(true);
                        
                        CriticalTimeScaleCon();
                        GameManager.inst.vfx.SpawnVFX(3, mon.transform.position);
                        SoundManager.Instance.Play(Enum_Sound.Effect, "Sound_CriticalHit");
                    }
                    else
                    {
                        SoundManager.Instance.PlayRandomAttack();
                        mon.Hit(true);
                    }
                    hero.Attack();
                    AttackSuccess(GameManager.inst.uiTimerbar.GetTimerSliderValue());
                }
                else
                {
                    StartCoroutine(mon.Action_AtkMove(hero.existPos, mon.existPos - 1));
                    hero.Hit();
                    AttackFail();

                    int ranNum = Random.Range(0, 4);
                    mon.stateBar.SettingState(mon.stateCount, ranNum);
                }


            }
            else if (mon.stateBar.stateType[mon.stateCount] == 3)
            {
                //mon.Move(mon.existPos + 1);

                int monNextMove = (mon.existPos + 1 == 3) ? 0 : mon.existPos + 1;

                if (hero.existPos == monNextMove)
                {
                    mon.Move(mon.existPos + 1);
                    
                    if (GameManager.RandomRate(0, hero.criticalRate) == 1)
                    {
                        mon.criticalHit = true;
                        mon.Hit(true);

                        CriticalTimeScaleCon();
                        GameManager.inst.vfx.SpawnVFX(3, mon.transform.position);
                        SoundManager.Instance.Play(Enum_Sound.Effect, "Sound_CriticalHit");
                    }
                    else
                    {
                        mon.Hit(true);
                    }
                    hero.Attack();
                    AttackSuccess(GameManager.inst.uiTimerbar.GetTimerSliderValue());
                }
                else
                {
                    StartCoroutine(mon.Action_AtkMove(hero.existPos, mon.existPos + 1));
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
