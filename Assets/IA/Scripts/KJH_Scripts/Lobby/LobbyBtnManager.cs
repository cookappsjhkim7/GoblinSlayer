using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LobbyBtnManager : MonoBehaviour
{
    public Button btnBuff;
    public ParticleSystem ptBuffOn;
    public Image weaponImage;
    public HeroController lobbyHero;


    int equipNum = 0;

    public void Awake()
    {
        equipNum = 0;
        GameManager.inst.hero.weapon.sprite = weaponImage.sprite;
    }

    public void OnInGameBtn()
    {
        SceneManager.LoadScene("InGame");

        LobbyManager.inst.stat.StatBuff(
            LobbyManager.inst.weaponData.weapon[equipNum].buff_hp,
            LobbyManager.inst.weaponData.weapon[equipNum].buff_shield,
            LobbyManager.inst.weaponData.weapon[equipNum].buff_criticalRate,
            LobbyManager.inst.weaponData.weapon[equipNum].buff_timeOver,
            LobbyManager.inst.weaponData.weapon[equipNum].buff_berserkGague
        );
    }

    public void OnWeaponNextBtn()
    {
        if (equipNum < LobbyManager.inst.weaponData.weapon.Count - 1)
        {
            weaponImage.sprite = LobbyManager.inst.weaponData.weapon[++equipNum].Sprite;
            //Debug.Log(equipNum + " < " + (LobbyManager.inst.weaponData.weapon.Count - 1));
            lobbyHero.weapon.sprite = weaponImage.sprite;
            LobbyManager.inst.weaponData.equipNum = equipNum;
        }
    }

    public void OnWeaponPreviousBtn()
    {
        if (equipNum > 0)
        {
            weaponImage.sprite = LobbyManager.inst.weaponData.weapon[--equipNum].Sprite;
            //Debug.Log(equipNum + " > 0");
            lobbyHero.weapon.sprite = weaponImage.sprite;
            LobbyManager.inst.weaponData.equipNum = equipNum;
        }
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
