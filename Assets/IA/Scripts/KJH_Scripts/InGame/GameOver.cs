using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public UI_TextManager uiTextManager;
    public Text killCountText;
    public Text coinCountText;

    private void OnEnable()
    {
        coinCountText.text = uiTextManager.coinCountText.text;
        killCountText.text = uiTextManager.killCountText.text;

        GameManager.Instance.hero.CharacterSpecDefault();
        LobbyManager.Instance.stat.SetStat(2, 1, 10, 2, 1);
        LobbyManager.Instance.SaveGame();
    }
}
