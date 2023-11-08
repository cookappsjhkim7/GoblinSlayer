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

    public int hpCount;
    public int shieldCount;
    public int[] criticalRate;

    public int existPos;
    WaitForSeconds wfsHitMask = new WaitForSeconds(0.2f);

    int movePosIndex;

    public ParticleSystem ptAtk;
    public ParticleSystem ptBerserkerAtk;


    public Transform chaImage;

    void Start()
    {
        myPos = new Vector2[3];
        myPos[0] = new Vector2(-1.2f, transform.position.y);
        myPos[1] = new Vector2(0f, transform.position.y);
        myPos[2] = new Vector2(1.2f, transform.position.y);

        criticalRate = new int[2];

        Move(1);

        //StartCoroutine_CoMove(0);

        CharacterSpecSetting(1, 2, 10);
    }


    public void CharacterSpecSetting(int _hp, int _shield, int _critical)
    {
        hpCount = _hp;
        shieldCount = _shield;
        criticalRate[0] = 100 - _critical;
        criticalRate[1] = _critical;

        for (int i = 0; i < hpCount; i++)
        {
            hp.SettingState(i, 0);
        }

        for (int i = 0; i < shieldCount; i++)
        {
            shield.SettingState(i, 0);
        }
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, myPos[movePosIndex], 40f * Time.deltaTime);
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
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, myPos[n], 1f * Time.deltaTime);

            if (transform.position.x == myPos[n].x)
            {
                yield break;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    public void Hit()
    {
        //if (shieldCount == 0)
        //{
        //    SoundManager.Instance.Play(Enum_Sound.Effect, "Sound_Kill");

        //    hp.stateSlot[hpCount - 1].gameObject.SetActive(false);
        //    hpCount--;

        //    StartCoroutine(CoHitMask());

        //    if (hpCount == 0)
        //    {
        //        gameOverMask.SetActive(true);
        //        GameManager.inst.uiTimerbar.TimerStop();
        //        //GameManager.inst.spawn.SpawnNextMonster();
        //    }
        //}
        //else
        //{
        //    StartCoroutine(CoShieldMask());
        //    shield.stateSlot[shieldCount - 1].gameObject.SetActive(false);
        //    shieldCount--;
        //}
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

    public void Attack()
    {
        ptAtk.Play();
        StartCoroutine(CoAttack());
    }

    public void BerserkerAttack()
    {
        ptBerserkerAtk.Play();
        StartCoroutine(CoBerserkerAttack());
    }

    IEnumerator CoAttack()
    {
        while (true)
        {
            chaImage.transform.Rotate(new Vector3(0, 0, -1) * 1800 * Time.deltaTime);

            if (chaImage.transform.rotation.eulerAngles.z < 120)
            {
                chaImage.transform.rotation = Quaternion.Euler(0, 0, 0);
                yield break;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator CoBerserkerAttack()
    {
        while (true)
        {
            chaImage.transform.Rotate(new Vector3(0, 0, -1) * 1800 * Time.deltaTime);

            if (!GameManager.inst.uiBerGauge.isBerserker)
            {
                ptBerserkerAtk.Stop();
                chaImage.transform.rotation = Quaternion.Euler(0, 0, 0);
                yield break;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
