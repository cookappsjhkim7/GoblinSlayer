using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class TutoHeroController : HeroController
{
    //Vector2[] myPos;

    //public GameObject hitMask;
    //public GameObject shieldMask;
    //public GameObject gameOverMask;
    //public UI_StateBar hp;
    //public UI_StateBar shield;

    //public int hpCount;
    //public int shieldCount;
    //public int[] criticalRate;

    //public int existPos;
    //WaitForSeconds wfsHitMask = new WaitForSeconds(0.2f);

    //int movePosIndex;

    //public ParticleSystem ptAtk;
    //public ParticleSystem ptBerserkerAtk;

    //public Transform chaImage;

    //public SpriteRenderer weapon;

    void Start()
    {
        myPos = new Vector2[3];
        myPos[0] = new Vector2(-1.2f, transform.position.y);
        myPos[1] = new Vector2(0f, transform.position.y);
        myPos[2] = new Vector2(1.2f, transform.position.y);

        criticalRate = new int[2];

        Move(1);

        weapon.sprite = LobbyManager.inst.weaponData.weaponList[0].Sprite;

        /* 디폴트 값 CharacterSpecSetting(1, 2, 10, 2, 1);
         * 1. hp
         * 2. shield : hp랑 동일하지만 실패 시 버서커게이지 감소 막아줌
         * 3. critial : 100% 기준으로 계산
         * 4. timeOver : 타임바 길이, 클 수록 좋음
         * 5. berserkerGague : 버서커 게이지 총 량, 작을 수록 좋음
         */
        //CharacterSpecDefault();
        //CharacterSpecDefault();

        hpCount = 2;
        shieldCount = 5;
        criticalRate[0] = 90;
        criticalRate[1] = 20;

        GameManager.inst.uiTimerbar.timeOver = 2;
        GameManager.inst.uiBerGauge.berserkGague = 1;

        for (int i = 0; i < shieldCount; i++)
        {
            shield.SettingState(i, 0);
        }
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, myPos[movePosIndex], 40f * Time.deltaTime);
    }

    public override void Hit()
    {
        SoundManager.Instance.Play(Enum_Sound.Effect, "Sound_Kill");
        StartCoroutine(CoHitMask());

        if (shieldCount != 0)
        {
            StartCoroutine(CoShieldMask());
            shield.stateSlot[shieldCount - 1].gameObject.SetActive(false);
            shieldCount--;
        }
    }
}
