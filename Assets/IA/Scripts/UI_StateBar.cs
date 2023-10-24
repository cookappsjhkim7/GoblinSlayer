using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StateBar : MonoBehaviour
{
    [System.Serializable]
    public class StateData
    {
        public Sprite stateTex;
        public int stateType;
    }

    public List<StateData> stateData;
    public int[] stateRate;
    
    public Image[] stateSlot;
    public int[] stateCountRate;
    public int[] stateType;
    



    public void SettingState(int stateNum, int index)
    {
        stateSlot[stateNum].gameObject.SetActive(true);
        stateSlot[stateNum].sprite = stateData[index].stateTex;
        stateType[stateNum] = index;
    }

    public void StateOff(int stateNum)
    {
        stateSlot[stateNum].gameObject.SetActive(false);
    }

    public int SpawnState()
    {
        int ranNum;
        int count = GameManager.RandomRate(0, stateCountRate);

        for (int i = count; i >= 0; i--)
        {
            ranNum = GameManager.RandomRate(0, stateRate);
            SettingState(i, ranNum);
        }

        return count;
    }
}
