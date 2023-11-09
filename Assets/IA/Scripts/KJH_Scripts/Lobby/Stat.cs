using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    public int hp = 2;
    public int shield = 1;
    public int criticalRate = 10;
    public float timeOver = 2;
    public float berserkGague = 1;
    
    public int hpBuff = 0;
    public int shieldBuff = 0;
    public int criticalRateBuff = 0;
    public float timeOverBuff = 0;
    public float berserkGagueBuff = 0;

    public void Awake()
    {
        hp = 2;
        shield = 1;
        criticalRate = 10;
        timeOver = 2;
        berserkGague = 1;
    }

    public void ResetStatBuff()
    {
        hpBuff = 0;
        shieldBuff = 0;
        criticalRateBuff = 0;
        timeOverBuff = 0;
        berserkGagueBuff = 0;
    }

    public void StatBuff(int _hp, int _shield, int _critical, float _timeOver, float _berserkGague)
    {
        hpBuff = _hp;
        shieldBuff = _shield;
        criticalRateBuff = _critical;
        timeOverBuff = _timeOver;
        berserkGagueBuff = _berserkGague;
    }

    public void SetStat(int _hp, int _shield, int _critical, float _timeOver, float _berserkGague)
    {
        hp = _hp + hpBuff;
        shield = _shield + shieldBuff;
        criticalRate = _critical + criticalRateBuff;
        timeOver = _timeOver + timeOverBuff;
        berserkGague = _berserkGague + berserkGagueBuff;
    }

}
