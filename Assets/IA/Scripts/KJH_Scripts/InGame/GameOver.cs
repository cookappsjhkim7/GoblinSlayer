using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public UI_TextManager uiTextManager;
    public Text killCountText;
    public Text coinCountText;
    public GameObject adButton;

    private void OnEnable()
    {
        coinCountText.text = uiTextManager.coinCountText.text;
        killCountText.text = uiTextManager.killCountText.text;
        
        adButton.gameObject.SetActive(true);

        GameManager.Instance.hero.CharacterSpecDefault();
        LobbyManager.Instance.stat.SetStat(2, 1, 10, 2, 1);
        LobbyManager.Instance.SaveGame();
    }

    public void OnClickADButton()
    {
        AdManager.Instance.TryShowRequest(() =>
        {
            GameManager.Instance.uiCombotex.WatchCoinAD();

            var coin = GameManager.Instance.uiCombotex.earnCoin;

            coinCountText.text += $"(+ {coin})";
            
            adButton.gameObject.SetActive(false);
            
            LobbyManager.Instance.SaveGame();
        });
    }
}
