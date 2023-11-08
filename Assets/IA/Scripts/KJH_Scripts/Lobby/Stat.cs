using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    public int buff_hp = 0;
    public int buff_shield = 0;
    public int buff_criticalRate = 0;
    public float buff_timeOver = 0;
    public float buff_berserkGague = 0;

    public void Awake()
    {
        buff_hp = 0;
        buff_shield = 0;
        buff_criticalRate = 0;
        buff_timeOver = 0;
        buff_berserkGague = 0;
    }

    public void StatBuff(int _hp, int _shield, int _critical, float _timeOver, float _berserkGague)
    {
        buff_hp += _hp;
        buff_shield += _shield;
        buff_criticalRate += _critical;
        buff_timeOver += _timeOver;
        buff_berserkGague += _berserkGague;
    }

}
