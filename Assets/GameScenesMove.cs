using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScenesMove : MonoBehaviour
{

    public void OnGameStartBtnClick()
    {
        SceneManager.LoadScene("InGame");
        Debug.Log("½ÇÇà");
    }

}
