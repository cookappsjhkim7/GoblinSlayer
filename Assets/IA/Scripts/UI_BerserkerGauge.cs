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
        gauge += num / 30;

        if (gauge > berserkGague)
        {
            gauge = 1;
            isBerserker = true;
            StartCoroutine("CoBerserkerMode", 5);
        }

        slider.value = gauge / 1;
    }
    public void GaugeDown()
    {
        gauge = slider.value - 0.1f;
        slider.value = gauge / 1;
    }

    IEnumerator CoBerserkerMode(int endTime)
    {
        Time.timeScale = 0.4f;

        yield return new WaitForSeconds(0.1f);

        Time.timeScale = 1.2f;

        berserkerMask.SetActive(true);

        tmp_berserkerMode.SetActive(true);

        GameManager.inst.uiTimerbar.TimerStop();

        GameManager.inst.hero.BerserkerAttack();
        SoundManager.Instance.Play(Enum_Sound.Bgm, "Sound_Berserk");
        while (true)
        {
            timer += Time.deltaTime;

            if (timer > endTime + 0.1f)
            {
                Time.timeScale = 1f;
                gauge = 0;
                slider.value = 0;
                isBerserker = false;
                berserkerMask.SetActive(false);
                tmp_berserkerMode.SetActive(false);
                GameManager.inst.uiTimerbar.TimerReset();
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
