using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public UI_StateBar stateBar;
    public int existPos;

    public Vector2 myPos = new Vector2();
    public int stateCount;

    public float moveSpeed = 1;

    public ParticleSystem ptSlash;
    public ParticleSystem ptCoin;

    public Transform hero;

    float movePosX;
    float xGap = 1.2f;

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

    //private void Update()
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -2.5f), moveSpeed * Time.deltaTime);
    //}

    public void Move(int index)
    {
        switch (index)
        {
            case -1:
                //transform.position = new Vector2(1.5f, transform.position.y);
                myPos.x = -1.5f;
                existPos = 2;
                StartCoroutine(CoMoveSide_02(2));
                break;

            case 0:
                //transform.position = new Vector2(-1.5f, transform.position.y);
                myPos.x = -xGap;       
                existPos = 0;
                StartCoroutine(CoMoveSide());
                break;

            case 1:
                //transform.position = new Vector2(0, transform.position.y);
                myPos.x = 0;
                existPos = 1;
                StartCoroutine(CoMoveSide());
                break;

            case 2:
                //transform.position = new Vector2(1.5f, transform.position.y);
                myPos.x = xGap;
                existPos = 2;
                StartCoroutine(CoMoveSide());
                break;

            case 3:
                //transform.position = new Vector2(-1.5f, transform.position.y);
                myPos.x = xGap + 0.3f;
                existPos = 0;
                StartCoroutine(CoMoveSide_02(0));
                break;
        }
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

    IEnumerator CoMoveSide()
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

    IEnumerator CoMoveSide_02(int nextMove)
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
            if (stateBar.stateType[0] == 1)
            {
                stateBar.SettingState(0, 0);
                stateCount++;
            }
            else
            {
                BattleCamera.Instance.Shake(0.1f,0.25f);
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

    public IEnumerator CoMoveAtk(Vector3 startPos, Vector3 endPos)
    {
        Debug.Log("Ω««‡");

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, 60f * Time.deltaTime);

            yield return new WaitForEndOfFrame();

            if (transform.position.y == endPos.y)
            {
                ptSlash.Play();
                endPos = startPos;
            }

            if (transform.position.y == startPos.y)
            {
                Debug.Log("≈ª√‚");
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

    public void Attack(bool moveAtk, Vector3 monPos, Vector3 heroPos)
    {
        StartCoroutine(CoMoveAtk(monPos, heroPos));

        //switch (moveAtk)
        //{
        //    case true:
        //        StartCoroutine(CoMoveAtk(monPos, heroPos));
        //        break;

        //    case false:
        //        StartCoroutine(CoMoveAtk(monPos, new Vector3(monPos.x, heroPos.y, 0)));
        //        break;
        //}
    }


}
