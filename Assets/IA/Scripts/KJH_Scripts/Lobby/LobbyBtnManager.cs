using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LobbyBtnManager : SingletonMonoBehaviour<LobbyBtnManager>
{
    public Button btnBuff;
    public ParticleSystem ptBuffOn;
    public Image weaponImage;
    public SpriteRenderer lobbyHero;
    public Text coin;
    public Text maxScore;

    public Text[] statTest;

    int equipNum = 0;

    bool buffOn = false;

    public void Awake()
    {
        equipNum = 0;
    }

    public void Start()
    {
        Refresh();

        var bgmRandom = Random.Range(0, 2);
        SoundManager.Instance.Play(Enum_Sound.Bgm, $"Sound_LobbyBGM{bgmRandom}");
    }

    public void OnEnable()
    {
        Refresh();
    }

    public void OnInGameBtn()
    {
        SceneManager.LoadScene("InGame");
        
        LobbyManager.Instance.stat.SetStat(
            LobbyManager.Instance.weaponData.weaponList[equipNum].hp,
            LobbyManager.Instance.weaponData.weaponList[equipNum].shield,
            LobbyManager.Instance.weaponData.weaponList[equipNum].criticalRate,
            LobbyManager.Instance.weaponData.weaponList[equipNum].timeOver,
            LobbyManager.Instance.weaponData.weaponList[equipNum].berserkGague
        );

        if (buffOn == true)
        {
            int n = Random.Range(0, 6);

            switch (n)
            {
                case 0:
                    LobbyManager.Instance.stat.StatBuff(1, 0, 0, 0, 0);
                    break;
                case 1:
                    LobbyManager.Instance.stat.StatBuff(0, 1, 0, 0, 0);
                    break;
                case 2:
                    LobbyManager.Instance.stat.StatBuff(0, 0, 10, 0, 0);
                    break;
                    LobbyManager.Instance.stat.StatBuff(0, 0, 0, 0.2f, 0);
                case 3:
                    LobbyManager.Instance.stat.StatBuff(0, 0, 0, 0, -0.1f);
                    break;
                case 4:
                    LobbyManager.Instance.stat.StatBuff(0, 1, 10, 0.2f, -0.1f);
                    break;
            }

            Debug.Log("버프 뭐 받았는지 보여줘야 해요" + n);

            // 버프 뭐 받았는지 보여줘야 해요
        }
    }

    public void OnWeaponNextBtn()
    {
        var weaponCount = LobbyManager.Instance.weaponData.weaponList.Count;
        equipNum++;
        
        if (equipNum >= weaponCount)
        {
            equipNum = 0;
        }
        
        Refresh();
    }

    public void OnWeaponPreviousBtn()
    {
        var weaponCount = LobbyManager.Instance.weaponData.weaponList.Count;
        equipNum--;
        
        if (equipNum < 0)
        {
            equipNum = weaponCount - 1;
        }
        
        Refresh();
    }

    private void Refresh()
    {
        weaponImage.sprite = LobbyManager.Instance.weaponData.weaponList[equipNum].Sprite;
        lobbyHero.sprite = weaponImage.sprite;
        LobbyManager.Instance.weaponData.equipNum = equipNum;
        coin.text = $"Coin : {LobbyManager.Instance.SaveData.currency}";
        maxScore.text = $"Max Score\n{LobbyManager.Instance.SaveData.maxScore}";

        statTest[0].text = LobbyManager.Instance.weaponData.weaponList[equipNum].hp.ToString();
        statTest[1].text = LobbyManager.Instance.weaponData.weaponList[equipNum].shield.ToString();
        statTest[2].text = LobbyManager.Instance.weaponData.weaponList[equipNum].criticalRate.ToString();
        statTest[3].text = LobbyManager.Instance.weaponData.weaponList[equipNum].timeOver.ToString();
        statTest[4].text = LobbyManager.Instance.weaponData.weaponList[equipNum].berserkGague.ToString();
    }


    // 광고 나오는 버튼
    public void OnBuffBtn()
    {
        Debug.Log("광고 나오게 해주세요");
        buffOn = true;
        btnBuff.interactable = false;
        ptBuffOn.Play();
        Debug.Log("버프 작동");
    }
}
