using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class HeroController : MonoBehaviour
{
    protected Vector2[] myPos;

    public GameObject hitMask;
    public GameObject shieldMask;
    public GameObject gameOverMask;
    public UI_StateBar hp;
    public UI_StateBar shield;

    public int hpCount;
    public int shieldCount;
    public int[] criticalRate;

    public int existPos;
    protected WaitForSeconds wfsHitMask = new WaitForSeconds(0.2f);

    protected int movePosIndex;

    public ParticleSystem ptAtk;
    public ParticleSystem ptBerserkerAtk;

    public Transform chaImage;

    public SpriteRenderer weapon;

    void Start()
    {
        myPos = new Vector2[3];
        myPos[0] = new Vector2(-1.2f, transform.position.y);
        myPos[1] = new Vector2(0f, transform.position.y);
        myPos[2] = new Vector2(1.2f, transform.position.y);

        criticalRate = new int[2];

        Move(1);

        weapon.sprite = LobbyManager.inst.weaponData.weaponList[LobbyManager.inst.weaponData.equipNum].Sprite;

        /* 디폴트 값 CharacterSpecSetting(1, 2, 10, 2, 1);
         * 1. hp
         * 2. shield : hp랑 동일하지만 실패 시 버서커게이지 감소 막아줌
         * 3. critial : 100% 기준으로 계산
         * 4. timeOver : 타임바 길이, 클 수록 좋음
         * 5. berserkerGague : 버서커 게이지 총 량, 작을 수록 좋음
         */
        //CharacterSpecDefault();
        CharacterSpecSetting(
            LobbyManager.inst.stat.hp,
            LobbyManager.inst.stat.shield,
            LobbyManager.inst.stat.criticalRate,
            LobbyManager.inst.stat.timeOver,
            LobbyManager.inst.stat.berserkGague
        );

        for (int i = 0; i < hpCount; i++)
        {
            hp.SettingState(i, 0);
        }
        for (int i = 0; i < shieldCount; i++)
        {
            shield.SettingState(i, 0);
        }
    }

    public void CharacterSpecDefault()
    {
        hpCount = 2;
        shieldCount = 1;
        criticalRate[0] = 90;
        criticalRate[1] = 10;

        GameManager.inst.uiTimerbar.timeOver = 2;
        GameManager.inst.uiBerGauge.berserkGague = 1;
    }

    public void CharacterSpecSetting(int _hp, int _shield, int _critical, float _timeOver, float _berserkGague)
    {
        hpCount = _hp;
        shieldCount = _shield;
        criticalRate[1] = _critical;
        criticalRate[0] = 100 - criticalRate[1];

        GameManager.inst.uiTimerbar.timeOver = _timeOver;
        GameManager.inst.uiBerGauge.berserkGague = _berserkGague;
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

    public virtual void Hit()
    {
        //Debug.LogError($"{name} Hit");

        if (shieldCount == 0)
        {
            SoundManager.Instance.Play(Enum_Sound.Effect, "Sound_Kill");

            hp.stateSlot[hpCount - 1].gameObject.SetActive(false);
            hpCount--;

            StartCoroutine(CoHitMask());

            if (hpCount == 0)
            {
                gameOverMask.SetActive(true);
                GameManager.inst.uiTimerbar.TimerStop();
                //GameManager.inst.spawn.SpawnNextMonster();
            }
        }
        else
        {
            StartCoroutine(CoShieldMask());
            shield.stateSlot[shieldCount - 1].gameObject.SetActive(false);
            shieldCount--;
        }
    }

    protected IEnumerator CoHitMask()
    {
        hitMask.SetActive(true);
        yield return wfsHitMask;
        hitMask.SetActive(false);
    }
    protected IEnumerator CoShieldMask()
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
        StartCoroutine(CoBerserkerAttack());
        ptBerserkerAtk.Play();
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
