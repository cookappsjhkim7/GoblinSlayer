using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    //public SpriteRenderer[] weaponImage;

    public int equipNum;

    [System.Serializable]
    public class Weapon
    {
        public bool isBbuy;
        public Sprite Sprite;
        public int price;
        public int buff_hp;
        public int buff_shield;
        public int buff_criticalRate;
        public float buff_timeOver;
        public float buff_berserkGague;
    }

    public List<Weapon> weaponList;


}
