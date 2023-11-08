using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager inst;
    public WeaponData weaponData;
    public Stat stat;

    private void Awake()
    {
        inst = this;
    }
}