using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    Vector2[] myPos;

    public GameObject hitMask;
    public GameObject shieldMask;
    public GameObject gameOverMask;
    public UI_StateBar hp;
    public UI_StateBar shield;

    int hpCount;
    int shieldCount;
    public int existPos;
    WaitForSeconds wfsHitMask = new WaitForSeconds(0.2f);

    int movePosIndex;

    void Start()
    {
        myPos = new Vector2[3];
        myPos[0] = new Vector2(-1.2f, transform.position.y);
        myPos[1] = new Vector2(0f, transform.position.y);
        myPos[2] = new Vector2(1.2f, transform.position.y);

        Move(1);

        //StartCoroutine_CoMove(0);

        shieldCount = 1;
        for (int i = 0; i < shieldCount; i++)
        {
            shield.SettingState(i, 0);
        }

        hpCount = 2;

        for(int i = 0; i<hpCount; i++)
        {
            hp.SettingState(i, 0);
        }
    }


    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, myPos[movePosIndex], 60f * Time.deltaTime);
    }
    public void Move(int n)
    {
        //transform.position = myPos[n];
        //transform.position = Vector2.MoveTowards(transform.position, myPos[n], 1f * Time.deltaTime);
        //StartCoroutine(CoMove(n));
        movePosIndex = n;
        existPos = n;
    }

    IEnumerator CoMove(int n)
    {
        while(true)
        {
            transform.position = Vector3.MoveTowards(transform.position, myPos[n], 1f * Time.deltaTime);

            if(transform.position.x == myPos[n].x)
            {
                yield break;
            }
        }
    }

    public void Hit()
    {
        if (shieldCount == 0)
        {
            hp.stateSlot[hpCount - 1].gameObject.SetActive(false);
            hpCount--;

            StartCoroutine(CoHitMask());

            if (hpCount == 0)
            {
                gameOverMask.SetActive(true);
                GameManager.inst.uiTimerbar.TimerStop();
            }
        }
        else
        {
            StartCoroutine(CoShieldMask());
            shield.stateSlot[shieldCount - 1].gameObject.SetActive(false);
            shieldCount--;
        }
    }

    IEnumerator CoHitMask()
    {
        hitMask.SetActive(true);
        yield return wfsHitMask;
        hitMask.SetActive(false);
    }
    IEnumerator CoShieldMask()
    {
        shieldMask.SetActive(true);
        yield return wfsHitMask;
        shieldMask.SetActive(false);
    }
}
