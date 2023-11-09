using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LobbyBtnManager : SingletonMonoBehaviour<LobbyBtnManager>
{
    public Button btnBuff;
    public ParticleSystem ptBuffOn;
    public Image weaponImage;
    public HeroController lobbyHero;
    public Text coin;

    public Text[] statTest;

    int equipNum = 0;

    public void Awake()
    {
        equipNum = 0;
    }

    public void Init()
    {
        Refresh();
    }

    public void OnInGameBtn()
    {
        SceneManager.LoadScene("InGame");
        
        LobbyManager.inst.stat.StatBuff(
            LobbyManager.inst.weaponData.weaponList[equipNum].hp,
            LobbyManager.inst.weaponData.weaponList[equipNum].shield,
            LobbyManager.inst.weaponData.weaponList[equipNum].criticalRate,
            LobbyManager.inst.weaponData.weaponList[equipNum].timeOver,
            LobbyManager.inst.weaponData.weaponList[equipNum].berserkGague
        );
    }

    public void OnWeaponNextBtn()
    {
        var weaponCount = LobbyManager.inst.weaponData.weaponList.Count;
        equipNum++;
        
        if (equipNum >= weaponCount)
        {
            equipNum = 0;
        }
        
        Refresh();
    }

    public void OnWeaponPreviousBtn()
    {
        var weaponCount = LobbyManager.inst.weaponData.weaponList.Count;
        equipNum--;
        
        if (equipNum < 0)
        {
            equipNum = weaponCount - 1;
        }
        
        Refresh();
    }

    private void Refresh()
    {
        weaponImage.sprite = LobbyManager.inst.weaponData.weaponList[equipNum].Sprite;
        lobbyHero.weapon.sprite = weaponImage.sprite;
        LobbyManager.inst.weaponData.equipNum = equipNum;
        coin.text = $"Coin : {GameManager.Instance.SaveData.currency}";

        statTest[0].text = LobbyManager.inst.weaponData.weaponList[equipNum].hp.ToString();
        statTest[1].text = LobbyManager.inst.weaponData.weaponList[equipNum].shield.ToString();
        statTest[2].text = LobbyManager.inst.weaponData.weaponList[equipNum].criticalRate.ToString();
        statTest[3].text = LobbyManager.inst.weaponData.weaponList[equipNum].timeOver.ToString();
        statTest[4].text = LobbyManager.inst.weaponData.weaponList[equipNum].berserkGague.ToString();
    }


    // 광고 나오는 버튼
    public void OnBuffBtn()
    {
        // 광고 나온 후
        LobbyManager.inst.stat.StatBuff(0, 1, 10, 0.2f, -0.1f);
        btnBuff.interactable = false;
        ptBuffOn.Play();
        Debug.Log("버프 작동");
    }
}
