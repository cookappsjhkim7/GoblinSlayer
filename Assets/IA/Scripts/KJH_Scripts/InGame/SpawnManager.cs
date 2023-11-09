using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<MonsterController> spawnData;

    // �迭 ��ġ
    float startY = -2.8f;
    float yGap = 1.2f;
    float[] xGap = new float[3] { -1.2f, 0, 1.2f };

    MonsterController tmpMon;

    public int stateCount;

    public int[,] spawnRate = new int[10, 7]
    {
        { 1, 0, 0, 0, 0, 0, 0 },
        { 1, 1, 1, 0, 0, 0, 0 },
        { 1, 1, 1, 1, 0, 0, 0 },
        { 1, 1, 1, 1, 1, 0, 0 },
        { 0, 1, 1, 1, 1, 1, 0 },
        { 0, 1, 1, 1, 1, 1, 1 },
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

        tmpMon = GameManager.Instance.pool.Get(GameManager.RandomRate(0, curSpawnRate), new Vector2(xGap[ranNum], startY + (yGap * (spawnPosY + 1)))).GetComponent<MonsterController>();
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

                StartCoroutine(spawnData[i].CoMoveDown());

                //spawnData[i].transform.position = spawnData[i].myPos;
            }
            else
            {
                SpawnMonster(i);
            }
        }
    }
}