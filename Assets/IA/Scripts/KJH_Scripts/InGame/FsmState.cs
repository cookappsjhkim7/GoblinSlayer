using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FsmState : MonoBehaviour
{
    public enum CHARACTERSTATE
    {
        ATTACK,
        DEATH,
    }

    protected List<CHARACTERSTATE> stateList;

    protected void AddState(CHARACTERSTATE _changeState)
    {
        if (!stateList.Contains(_changeState))
        {
            stateList.Add(_changeState);
        }
    }

    protected void RemoveState(CHARACTERSTATE _changeState)
    {
        if (stateList.Contains(_changeState))
        {
            stateList.Remove(_changeState);
        }
    }
}
