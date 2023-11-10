using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    public void PlaySound()
    {
        SoundManager.Instance.Play(Enum_Sound.Effect, "Sound_Cow");
    }
}
