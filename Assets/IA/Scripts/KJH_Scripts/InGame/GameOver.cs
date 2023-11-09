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
        LobbyManager.Instance.SaveGame();
    }
}
