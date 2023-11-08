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

    // ���� ������ ��ư
    public void OnBuffBtn()
    {
        // ���� ���� ��
        Stat.StatBuff(0, 1, 10, 0.2f, -0.1f);
        btnBuff.interactable = false;
        ptBuffOn.Play();
        Debug.Log("���� �۵�");
    }
}
