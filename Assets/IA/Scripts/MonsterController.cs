using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public UI_StateBar stateBar;
    public int existPos;

    public Vector2 myPos = new Vector2();
    public int stateCount;

    float movePosX;

    //private void Start()
    //{
    //    Debug.Log(stateCount);
    //    stateCount = stateBar.SpawnState();
    //    Debug.Log(stateBar.SpawnState());
    //}

    private void OnEnable()
    {
        stateCount = stateBar.SpawnState();
    }

    public void Move(int index)
    {
        switch (index)
        {
            case -1:
                //transform.position = new Vector2(1.5f, transform.position.y);
                myPos.x = -3f;
                existPos = 2;
                StartCoroutine(CoMoving02(2));
                break;

            case 0:
                //transform.position = new Vector2(-1.5f, transform.position.y);
                myPos.x = -1.8f;       
                existPos = 0;
                StartCoroutine(CoMoving());
                break;

            case 1:
                //transform.position = new Vector2(0, transform.position.y);
                myPos.x = 0;
                existPos = 1;
                StartCoroutine(CoMoving());
                break;

            case 2:
                //transform.position = new Vector2(1.5f, transform.position.y);
                myPos.x = 1.8f;
                existPos = 2;
                StartCoroutine(CoMoving());
                break;

            case 3:
                //transform.position = new Vector2(-1.5f, transform.position.y);
                myPos.x = 3f;
                existPos = 0;
                StartCoroutine(CoMoving02(0));
                break;
        }
    }

    IEnumerator CoMoving()
    {
        Vector2 dest = new Vector2(myPos.x, transform.position.y);

        while (true)
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, dest, 60f * Time.deltaTime);
            yield return null;

            if(transform.position.x == dest.x)
            {
                yield break;
            }
        }
    }

    IEnumerator CoMoving02(int nextMove)
    {
        Vector2 dest = new Vector2(myPos.x, transform.position.y);
        Vector2 tmpDest;

        if (nextMove == 2)
        {
            tmpDest = new Vector2(3, transform.position.y);
        }
        else if(nextMove == 0)
        {
            tmpDest = new Vector2(-3, transform.position.y);
        }
        else
        {
            tmpDest = new Vector2(-100, transform.position.y);
        }
                
        while (true)
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, dest, 60f * Time.deltaTime);
            yield return null;

            if (transform.position.x == dest.x)
            {
                transform.position = tmpDest;
                Move(nextMove);
                yield break;
            }
        }
    }

    public void RemoveStateBar()
    {
        stateBar.stateSlot[stateCount].gameObject.SetActive(false);
        stateBar.stateData[stateCount].stateType = -1;
        
        if (stateCount == 0)
        {
            GameManager.inst.spawn.spawnData.RemoveAt(0);
            GameManager.inst.spawn.SpawnNextMonster();
            GameManager.inst.uiCombotex.KillCount();
            gameObject.SetActive(false);
            //StartCoroutine(CoDeath());
            //gameObject.SetActive(false);

            if (!GameManager.inst.uiBerGauge.isBerserker)
            {
                GameManager.inst.uiCombotex.lvCount();
            }

        }

        stateCount--;
    }

    IEnumerator CoDeath()
    {

        yield return new WaitForSeconds(0.15f);

        GameManager.inst.spawn.spawnData.RemoveAt(0);
        GameManager.inst.spawn.SpawnNextMonster();
        GameManager.inst.uiCombotex.KillCount();
        gameObject.SetActive(false);
    }

    public void Hit()
    {
        //isCombo = false;
        RemoveStateBar();
    }

    public void Attack()
    {
        //isCombo = true;
    }


}
