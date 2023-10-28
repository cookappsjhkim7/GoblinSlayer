using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public UI_StateBar stateBar;
    public int existPos;

    public Vector2 myPos = new Vector2();
    public int stateCount;

    public ParticleSystem ptSlash;
    public ParticleSystem ptCoin;

    //public Transform hero;

    bool isMove = false;
    bool isAtk = false;

    Vector3[] atkPos = {
            new Vector2(-1.2f, -2.5f),
            new Vector2(0f, -2.5f),
            new Vector2(1.2f, -2.5f)
         };

    private void OnEnable()
    {
        stateCount = stateBar.SpawnState();
    }


    public IEnumerator Action_AtkMove(int atkPosIndex, int movePosIndex)
    {
        while (true)
        {
            if (isMove == false)
            {
                Attack(atkPosIndex);
                break;
            }

            yield return new WaitForEndOfFrame();
        }
        
        while(true)
        {
            if(isAtk == false)
            {
                Move(movePosIndex);
                yield break;
            }

            yield return new WaitForEndOfFrame();
        }

    }
    public IEnumerator Action_MoveAtk(int movePosIndex, int atkPosIndex)
    {
        Move(movePosIndex);

        while (true)
        {
            if (isMove == false)
            {
                Attack(atkPosIndex);
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        while (true)
        {
            if (isAtk == false)
            {
                Move(atkPosIndex);
                yield break;
            }

            yield return new WaitForEndOfFrame();
        }
        
    }

    public void Attack(int atkPosIndex) // 0 바로 앞 공격, 1 추격 공격, 2 이동, 3 바로 앞 공격 후 이동, 4 추격 공격 후 이동
    {
        isAtk = true;
        StartCoroutine(CoAtk(atkPosIndex));
    }

    IEnumerator CoAtk(int atkPosIndex)
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, atkPos[atkPosIndex], 30f * Time.deltaTime);

            if (transform.position == atkPos[atkPosIndex])
            {
                isAtk = false;
                ptSlash.Play();
                yield break;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    public void Move(int movePosIndex)
    {
        isMove = true;

        switch (movePosIndex)
        {
            case -1:
                myPos.x = -1.5f;
                existPos = 2;
                StartCoroutine(CoMoveSideEscape(2));
                break;

            case 0:
                myPos.x = -1.2f;
                existPos = 0;
                StartCoroutine(CoMoveSide());
                break;

            case 1:
                myPos.x = 0;
                existPos = 1;
                StartCoroutine(CoMoveSide());
                break;

            case 2:
                myPos.x = 1.2f;
                existPos = 2;
                StartCoroutine(CoMoveSide());
                break;

            case 3:
                myPos.x = 1.5f;
                existPos = 0;
                StartCoroutine(CoMoveSideEscape(0));
                break;
        }
    }

    IEnumerator CoMoveSide()
    {
        Vector3 dest = new Vector3(myPos.x, -1.6f, 0);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, dest, 30f * Time.deltaTime);

            if (transform.position == dest)
            {
                isMove = false;
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator CoMoveSideEscape(int nextMove)
    {
        Vector3 dest = new Vector3(myPos.x, -1.6f, 0);
        Vector3 tmpDest;

        if (nextMove == 2)
        {
            tmpDest = new Vector3(3, -1.6f, 0);
        }
        else if (nextMove == 0)
        {
            tmpDest = new Vector3(-3, -1.6f, 0);
        }
        else
        {
            tmpDest = new Vector3(-100, -1.6f, 0);
        }

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, dest, 60f * Time.deltaTime);
            yield return null;

            if (transform.position == dest)
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
            if (stateBar.stateType[0] == 1)
            {
                stateBar.SettingState(0, 0);
                stateCount++;
            }
            else
            {
                BattleCamera.Instance.Shake(0.1f, 0.25f);
                SoundManager.Instance.Play(Enum_Sound.Effect, "Sound_Kill");
                GameManager.inst.spawn.spawnData.RemoveAt(0);
                GameManager.inst.spawn.SpawnNextMonster();
                GameManager.inst.uiCombotex.KillCount();

                //gameObject.SetActive(false);
                StartCoroutine(CoDeath());
            }

            if (!GameManager.inst.uiBerGauge.isBerserker)
            {
                GameManager.inst.uiCombotex.lvCount();
            }

        }

        stateCount--;
    }

    public IEnumerator CoMoveDown()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, myPos, 10f * Time.deltaTime);

            yield return new WaitForEndOfFrame();

            if (transform.position.y == myPos.y)
            {
                yield break;
            }
        }
    }

    IEnumerator CoDeath()
    {
        yield return new WaitForSeconds(0.1f);

        //GameManager.inst.spawn.spawnData.RemoveAt(0);
        //GameManager.inst.spawn.SpawnNextMonster();
        //GameManager.inst.uiCombotex.KillCount();

        gameObject.SetActive(false);
    }

    public void Hit()
    {
        //isCombo = false;
        RemoveStateBar();
    }
}
