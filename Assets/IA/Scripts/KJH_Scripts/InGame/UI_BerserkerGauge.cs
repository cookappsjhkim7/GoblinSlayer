using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BerserkerGauge : MonoBehaviour
{
    Slider slider;

    float gauge = 0;
    public float berserkGague = 1;
    float timer = 0;

    public bool isBerserker = false;
    public GameObject berserkerMask;

    public GameObject tmp_berserkerMode;

    void Start()
    {
        slider = GetComponent<Slider>();

        gauge = 0;
        slider.value = 0;
    }

    public void GaugeCharging(float num)
    {
        gauge += num / 40;

        if (gauge > berserkGague)
        {
            gauge = 1;
            isBerserker = true;
            StartCoroutine(CoBerserkerMode(5));
        }

        slider.value = gauge / 1;
    }
    public void GaugeDown()
    {
        gauge = slider.value - 0.1f;
        slider.value = gauge / 2;
    }

    IEnumerator CoBerserkerMode(int endTime)
    {
        berserkerMask.SetActive(true);
        tmp_berserkerMode.SetActive(true);
        
        GameManager.Instance.uiTimerbar.TimerStop();
        GameManager.Instance.hero.BerserkerAttack();
        
        SoundManager.Instance.Play(Enum_Sound.Bgm, "Sound_Berserk");

        while (true)
        {
            timer += Time.deltaTime;

            if (timer > endTime + 0.1f)
            {
                gauge = 0;
                slider.value = 0;
                isBerserker = false;
                berserkerMask.SetActive(false);
                tmp_berserkerMode.SetActive(false);
                GameManager.Instance.uiTimerbar.TimerReset();
                timer = 0;
                SoundManager.Instance.Play(Enum_Sound.Bgm, "Sound_PlayBGM");
                yield break;
            }

            slider.value = (endTime - timer) / endTime;

            yield return null;
        }
    }

    public void BerserkerModeStop()
    {
        StopCoroutine("CoBerserkerMode");
        isBerserker = false;
        berserkerMask.SetActive(false) ;
    }
}
