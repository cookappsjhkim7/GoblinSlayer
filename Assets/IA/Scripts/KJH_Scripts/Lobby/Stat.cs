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

    public void Awake()
    {
        hp = 2;
        shield = 1;
        criticalRate = 10;
        timeOver = 2;
        berserkGague = 1;
    }

    public void StatBuff(int _hp, int _shield, int _critical, float _timeOver, float _berserkGague)
    {
        hp += _hp;
        shield += _shield;
        criticalRate += _critical;
        timeOver += _timeOver;
        berserkGague += _berserkGague;
    }



    public void SetStat(int _hp, int _shield, int _critical, float _timeOver, float _berserkGague)
    {
        hp = _hp;
        shield = _shield;
        criticalRate = _critical;
        timeOver = _timeOver;
        berserkGague = _berserkGague;
    }

}
