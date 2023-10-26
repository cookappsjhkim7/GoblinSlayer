using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<MonsterController> spawnData;

    // 배열 위치
    float startY = -2.8f;
    float yGap = 1.2f;
    float[] xGap = new float[3] { -1.2f, 0, 1.2f };

    MonsterController tmpMon;

    public int stateCount;

    public int[,] spawnRate = new int[10, 7]
    {
        { 1, 0, 0, 0, 0, 0, 0 },
        { 1, 1, 0, 0, 0, 0, 0 },
        { 1, 1, 1, 0, 0, 0, 0 },
        { 1, 1, 1, 1, 0, 0, 0 },
        { 0, 1, 1, 1, 1, 0, 0 },
        { 0, 1, 1, 1, 1, 1, 0 },
        { 0, 0, 1, 1, 1, 1, 1 },
        { 0, 0, 0, 1, 1, 1, 1 },
        { 0, 0, 0, 0, 1, 1, 1 },
        { 0, 0, 0, 0, 0, 1, 1 }
    };

    int[] curSpawnRate = new int[7];

    private void Start()
    {
        ChangeSpawnRate(0);
        
        spawnData = new List<MonsterController>();

        for (int i = 0; i < 7; i++)
        {
            SpawnMonster(i);
        }
    }

    public void ChangeSpawnRate(int n)
    {
        for (int i = 0; i < 7; i++)
        {
            curSpawnRate[i] = spawnRate[n, i];
        }
    }

    public void SpawnMonster(int spawnPosY)
    {
        int ranNum = Random.Range(0, 3);
                
        tmpMon = GameManager.inst.pool.Get(GameManager.RandomRate(0, curSpawnRate), new Vector2(xGap[ranNum], startY + (yGap * (spawnPosY + 1)))).GetComponent<MonsterController>();
        tmpMon.existPos = ranNum;
        tmpMon.myPos = new Vector2(xGap[ranNum], startY + (yGap * (spawnPosY + 1)));

        //tmpMon.stateCount = RandomRate(0, GameManager.uiStateCountRate);
        //SpawnState(tmpMon, tmpMon.stateCount, ranNum);
        //SpawnState02(tmpMon, tmpMon.stateCount);

        spawnData.Add(tmpMon);
    }

    public void SpawnNextMonster()
    {
        for (int i = 0; i < 7; i++)
        {
            if (i + 1 < 7)
            {
                tmpMon = spawnData[i];
                tmpMon.myPos.y -= yGap;

                spawnData[i] = tmpMon;

                StartCoroutine(spawnData[i].CoMoveDown(spawnData[i]));

                //spawnData[i].transform.position = spawnData[i].myPos;
            }
            else
            {
                SpawnMonster(i);
            }
        }
    }

    //private void SpawnState(MonsterController mon, int stCount, int futureExistPos)
    //{
    //    int ranNum;

    //    for (int i = stCount; i >= 0; i--)
    //    {
    //        ranNum = RandomRate(0, GameManager.uiStateRate);

    //        if (futureExistPos == 0)
    //        {
    //            if (ranNum == 2)
    //            {
    //                while (true)
    //                {
    //                    ranNum = RandomRate(0, GameManager.uiStateRate);

    //                    if (ranNum != 2)
    //                    {
    //                        break;
    //                    }
    //                }
    //            }
    //            if (ranNum == 3)
    //            {
    //                futureExistPos += 1;
    //            }
    //        }
    //        else if (futureExistPos == 1)
    //        {
    //            if (ranNum == 2)
    //            {
    //                futureExistPos -= 1;
    //            }
    //            else if (ranNum == 3)
    //            {
    //                futureExistPos += 1;
    //            }
    //        }
    //        else if (futureExistPos == 2)
    //        {
    //            if (ranNum == 3)
    //            {
    //                while (true)
    //                {
    //                    ranNum = RandomRate(0, GameManager.uiStateRate);

    //                    if (ranNum != 3)
    //                    {
    //                        break;
    //                    }
    //                }
    //            }
    //            if (ranNum == 2)
    //            {
    //                futureExistPos -= 1;
    //            }
    //        }
    //        else
    //        {
    //            Debug.Log("몬스터 소환 위치 에러");
    //        }

    //        //mon.stateBar.Get(ranNum);
    //        mon.stateBar.SettingState(i, ranNum);
    //    }

    //}

    //private void SpawnState02(MonsterController mon, int stCount)
    //{
    //    int ranNum;

    //    for (int i = stCount; i >= 0; i--)
    //    {
    //        ranNum = Random.Range(0, 4);

    //        while (true)
    //        {
    //            if (mon.stateBar.stateTex[ranNum] == null)
    //            {
    //                ranNum = Random.Range(0, 4);
    //            }
    //            else
    //            {
    //                break;
    //            }
    //        }

    //        mon.stateBar.SettingState(i, ranNum);
    //    }
    //}
}