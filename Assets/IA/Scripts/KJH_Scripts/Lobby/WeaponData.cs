using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public bool isBbuy;
    public Sprite Sprite;
    public int price;
    public int hp;
    public int shield;
    public int criticalRate;
    public float timeOver;
    public float berserkGague;
}

public class WeaponData : MonoBehaviour
{
    //public SpriteRenderer[] weaponImage;

    public int equipNum;

    public List<Weapon> weaponList;
}
