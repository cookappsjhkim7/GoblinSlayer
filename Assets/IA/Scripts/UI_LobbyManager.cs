using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LobbyManager : MonoBehaviour
{
    public HeroController hero;

    public void OnStartBtnClick()
    {

    }

    private void GameReset()
    {
        GameManager.inst.spawn.spawnData = new List<MonsterController>();

        for (int i = 0; i < 7; i++)
        {
            GameManager.inst.spawn.SpawnMonster(i);
        }

        GameManager.inst.spawn.ChangeSpawnRate(0);

        hero.Move(1);

        GameManager.inst.uiTimerbar.WaitTimeReset();
        GameManager.inst.uiCombotex.KillReset();
    }
}