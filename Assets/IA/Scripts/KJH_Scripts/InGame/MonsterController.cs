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
    //public ParticleSystem ptHit;

    public GameObject CowHead;
    public GameObject NormalHead;

    protected int rCoin;

    protected bool isMove;
    protected bool isAtk;

    public bool criticalHit;

    protected Vector3[] atkPos = {
            new Vector2(-1.2f, -3f),
            new Vector2(0f, -3f),
            new Vector2(1.2f, -3f)
         };

    private void OnEnable()
    {
        CowHead.SetActive(LobbyManager.Instance.CowOn);
        NormalHead.SetActive(!LobbyManager.Instance.CowOn);
        isMove = false;
        isAtk = false;
        //isHit = false;
        criticalHit = false;
        stateCount = stateBar.SpawnState();
        transform.rotation = Quaternion.Euler(0, 0, 0);
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

        while (true)
        {
            if (isAtk == false)
            {
                Move(movePosIndex);
                yield break;
            }

            yield return new WaitForEndOfFrame();
        }

    }
   
    public void Attack(int atkPosIndex) 
    {
        SoundManager.Instance.Play(Enum_Sound.Effect, "Sound_MonsterAttack");
        isAtk = true;
        StartCoroutine(CoAtk(atkPosIndex));
    }

    IEnumerator CoAtk(int atkPosIndex)
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, atkPos[atkPosIndex], 40f * Time.deltaTime);

            if (transform.position == atkPos[atkPosIndex])
            {
                isAtk = false;
                ptSlash.Play();
                SoundManager.Instance.Play(Enum_Sound.Effect, "Sound_Miss");
                yield break;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    public void Move(int movePosIndex)
    {
        //Debug.Log(isMove);
        if (isMove == false)
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
                isMove = false;
                Move(nextMove);
                yield break;
            }
        }
    }

    public virtual void RemoveStateBar()
    {
        stateBar.stateSlot[stateCount].gameObject.SetActive(false);
        //stateBar.stateType[stateCount] = -1;

        if (!criticalHit)
        {
            if (stateCount == 0)
            {
                if (stateBar.stateType[0] == 1 && !GameManager.Instance.uiBerGauge.isBerserker)
                {
                    stateBar.SettingState(0, 0);
                    stateCount++;
                }
                else
                {
                    rCoin = Random.Range(0, 10);

                    if (rCoin == 0)
                    {
                        GameManager.Instance.vfx.SpawnVFX(1, transform.position);
                        GameManager.Instance.uiCombotex.CoinCount();
                        SoundManager.Instance.Play(Enum_Sound.Effect, "Sound_Coin",0.7f);
                    }

                    BattleCamera.Instance.Shake(0.1f, 0.25f);
                    GameManager.Instance.spawn.spawnData.RemoveAt(0);
                    GameManager.Instance.uiCombotex.KillCount();

                    //gameObject.SetActive(false);
                    GameManager.Instance.vfx.SpawnVFX_2(2, transform.position, 0);
                    StopAllCoroutines();
                    isMove = false;
                    isAtk = false;
                    criticalHit = false;
                    gameObject.SetActive(false);
                    //StartCoroutine(CoDeath());
                }

                if (!GameManager.Instance.uiBerGauge.isBerserker)
                {
                    GameManager.Instance.uiCombotex.lvCount();
                }
            }

            stateCount--;
        }
        else
        {
            int n = stateCount;

            for (int i = 0; i <= n; i++)
            {
                if (stateCount == 0)
                {
                    rCoin = Random.Range(0, 10);

                    if (rCoin == 0)
                    {
                        GameManager.Instance.vfx.SpawnVFX(1, transform.position);
                        GameManager.Instance.uiCombotex.CoinCount();
                        SoundManager.Instance.Play(Enum_Sound.Effect, "Sound_Coin");
                    }

                    BattleCamera.Instance.Shake(0.1f, 0.25f);
                    GameManager.Instance.spawn.spawnData.RemoveAt(0);
                    GameManager.Instance.uiCombotex.KillCount();

                    //gameObject.SetActive(false);
                    GameManager.Instance.vfx.SpawnVFX_2(2, transform.position, 0);
                    StopAllCoroutines();
                    isMove = false;
                    isAtk = false;
                    criticalHit = false;
                    gameObject.SetActive(false);
                    //StartCoroutine(CoDeath());
                }

                stateCount--;
            }
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

    protected IEnumerator CoDeath()
    {
        float rPosY = Random.Range(0, 5);
        float rPosX = Random.Range(2, 8);
        int rNum = Random.Range(0, 2);

        Vector3 pos = (rNum == 0) ? new Vector3(-rPosX, rPosY, 0) : new Vector3(rPosX, rPosY, 0);

        float dirWing = Mathf.Atan2(pos.y - transform.position.y, pos.x - transform.position.x) * 180 / Mathf.PI;

        GameManager.Instance.vfx.SpawnVFX_2(2, transform.position, dirWing - 90);

        while (true)
        {
            if (isMove == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, pos, 15f * Time.deltaTime);
                transform.Rotate(new Vector3(0, 0, 1) * 1000 * Time.deltaTime);

                if (transform.position == pos)
                {
                    gameObject.SetActive(false);
                    yield break;
                }
            }
            yield return new WaitForEndOfFrame();   
        }
    }

    public IEnumerator CoHit()
    {
        while (true)
        {
            if (isMove == false)
            {
                SoundManager.Instance.Play(Enum_Sound.Effect, "Sound_Kill");

                RemoveStateBar();
                yield break;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    public void Hit(bool move)
    {
        if (move)
        {
            StartCoroutine(CoHit());
        }
        else
        {
            SoundManager.Instance.Play(Enum_Sound.Effect, "Sound_Kill");
            //GameManager.inst.vfx.VFXPlay(GameManager.inst.vfx.ptHit, transform.position);
            GameManager.Instance.vfx.SpawnVFX(0, transform.position);
            RemoveStateBar();
        }
    }
}
