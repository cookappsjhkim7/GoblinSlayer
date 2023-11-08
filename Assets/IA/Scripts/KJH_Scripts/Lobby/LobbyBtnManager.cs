using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LobbyBtnManager : MonoBehaviour
{
    public Button btnBuff;
    public ParticleSystem ptBuffOn;

    public void OnInGameBtn()
    {
        SceneManager.LoadScene("InGame");
    }

    // 광고 나오는 버튼
    public void OnBuffBtn()
    {
        // 광고 나온 후
        Stat.StatBuff(0, 1, 10, 0.2f, -0.1f);
        btnBuff.interactable = false;
        ptBuffOn.Play();
        Debug.Log("버프 작동");
    }
}
