using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LobbyBtnManager : SingletonMonoBehaviour<LobbyBtnManager>
{
    [SerializeField] public GameObject tutorial;
    public Button btnBuff;
    public Button btnGuide;
    public ParticleSystem ptBuffOn;
    public Image weaponImage;
    public SpriteRenderer lobbyHero;
    public Text coin;
    public Text maxScore;

    [SerializeField] private GameObject goCost;
    [SerializeField] private GameObject goBuyBtn;
    [SerializeField] private GameObject goSelectBtn;
    [SerializeField] private Text txtCost;
    [SerializeField] private Text txtBuy;
    
    public Text[] statTest;

    int equipNum = 0;
    int selectedIndex = 0;

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
        selectedIndex = LobbyManager.Instance.weaponData.equipNum;
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
            // int n = Random.Range(0, 6);
            //
            // switch (n)
            // {
            //     case 0:
            //         LobbyManager.Instance.stat.StatBuff(1, 0, 0, 0, 0);
            //         break;
            //     case 1:
            //         LobbyManager.Instance.stat.StatBuff(0, 1, 0, 0, 0);
            //         break;
            //     case 2:
            //         LobbyManager.Instance.stat.StatBuff(0, 0, 10, 0, 0);
            //         break;
            //         LobbyManager.Instance.stat.StatBuff(0, 0, 0, 0.2f, 0);
            //     case 3:
            //         LobbyManager.Instance.stat.StatBuff(0, 0, 0, 0, -0.1f);
            //         break;
            //     case 4:
            //         LobbyManager.Instance.stat.StatBuff(0, 1, 10, 0.2f, -0.1f);
            //         break;
            // }

            Debug.Log("버프 뭐 받았는지 보여줘야 해요");

            btnBuff.interactable = true;
            buffOn = false;
            LobbyManager.Instance.stat.ResetStatBuff();

            // 버프 뭐 받았는지 보여줘야 해요
        }
    }

    public void OnWeaponNextBtn()
    {
        var weaponCount = LobbyManager.Instance.weaponData.weaponList.Count;
        selectedIndex++;
        
        if (selectedIndex >= weaponCount)
        {
            selectedIndex = 0;
        }
        
        Refresh();
    }

    public void OnWeaponPreviousBtn()
    {
        var weaponCount = LobbyManager.Instance.weaponData.weaponList.Count;
        selectedIndex--;
        
        if (selectedIndex < 0)
        {
            selectedIndex = weaponCount - 1;
        }
        
        Refresh();
    }

    private void Refresh()
    {
        weaponImage.sprite = LobbyManager.Instance.weaponData.weaponList[selectedIndex].Sprite;
        lobbyHero.sprite = LobbyManager.Instance.weaponData.weaponList[equipNum].Sprite;
        coin.text = $"{LobbyManager.Instance.SaveData.currency}";
        maxScore.text = $"Max Score\n{LobbyManager.Instance.SaveData.maxScore}";

        var hp = LobbyManager.Instance.weaponData.weaponList[selectedIndex].hp;
        statTest[0].text = hp.ToString();
        
        var shield = LobbyManager.Instance.weaponData.weaponList[selectedIndex].shield;
        statTest[1].text = shield.ToString();
        
        var criticalRate = LobbyManager.Instance.weaponData.weaponList[selectedIndex].criticalRate;
        statTest[2].text = criticalRate.ToString();
        
        var timeOver = LobbyManager.Instance.weaponData.weaponList[selectedIndex].timeOver;
        statTest[3].text = timeOver.ToString();
        
        var berserkGague = LobbyManager.Instance.weaponData.weaponList[selectedIndex].berserkGague;
        statTest[4].text = berserkGague.ToString();
        
        // BTN GROUP
        txtCost.text = $"{LobbyManager.Instance.weaponData.weaponList[selectedIndex].price}";
        var isBuy = LobbyManager.Instance.weaponData.weaponList[selectedIndex].isBbuy;
        goCost.SetActive(!isBuy);
        goBuyBtn.SetActive(!isBuy);
        goSelectBtn.SetActive(isBuy && LobbyManager.Instance.weaponData.equipNum != selectedIndex);
        var userCur = LobbyManager.Instance.SaveData.currency;
        var buyPrice = LobbyManager.Instance.weaponData.weaponList[selectedIndex].price;
        txtCost.color = userCur < buyPrice ? Color.red : Color.white;
        txtBuy.color = userCur < buyPrice ? Color.red : Color.white;

        if (buffOn)
        {
            var stat = LobbyManager.Instance.stat;
            if (stat.hpBuff > 0)
            {
                statTest[0].text = $"{hp + stat.hpBuff} +{stat.hpBuff}";
                statTest[0].color = Color.yellow;
            }

            if (stat.shieldBuff > 0)
            {
                statTest[1].text = $"{shield + stat.shieldBuff} +{stat.shieldBuff}";
                statTest[1].color = Color.yellow;
            }

            if (stat.criticalRateBuff > 0)
            {
                statTest[2].text = $"{criticalRate + stat.criticalRateBuff} +{stat.criticalRateBuff}";
                statTest[2].color = Color.yellow;
            }

            if (stat.timeOverBuff > 0)
            {
                statTest[3].text = $"{timeOver + stat.timeOverBuff} +{stat.timeOverBuff}";
                statTest[3].color = Color.yellow;
            }

            if (stat.berserkGagueBuff > 0)
            {
                statTest[4].text = $"{berserkGague + stat.berserkGagueBuff} +{stat.berserkGagueBuff}";
                statTest[4].color = Color.yellow;
            }
        }
        else
        {
            foreach (var text in statTest)
            {
                text.color = Color.white;
            }
        }
    }


    // 광고 나오는 버튼
    public void OnBuffBtn()
    {
        AdManager.Instance.TryShowRequest(() =>
        {
            Debug.Log("광고 나오게 해주세요");
            buffOn = true;
            btnBuff.interactable = false;
            ptBuffOn.Play();

            var rand = Random.Range(0, 5);
            
            switch (rand)
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
                case 3:
                    LobbyManager.Instance.stat.StatBuff(0, 0, 0, 0, -0.1f);
                    break;
                case 4:
                    LobbyManager.Instance.stat.StatBuff(0, 1, 10, 0.2f, -0.1f);
                    break;
            }
            
            Refresh();
            Debug.Log("버프 작동");
        });
    }

    public void OnClickBuyBtn()
    {
        var userCur = LobbyManager.Instance.SaveData.currency;
        var buyPrice = LobbyManager.Instance.weaponData.weaponList[selectedIndex].price;
        if (userCur < buyPrice)
        {
            return;
        }
        
        LobbyManager.Instance.weaponData.weaponList[selectedIndex].isBbuy = true;
        Refresh();
    }

    public void OnClickSelectBtn()
    {
        equipNum = selectedIndex;
        LobbyManager.Instance.weaponData.equipNum = equipNum;
        Refresh();
    }
    
    
    public void OnClickGuide()
    {
        tutorial.SetActive(true);
    }
}
