using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TimeCount : MonoBehaviour
{
    Slider slider;
    public HeroController hero;

    float timer;
    public float timeOver;
    float kipTimeOver;

    private void Start()
    {
        slider = GetComponent<Slider>();

        slider.value = 1;
        timeOver = 2f + Stat.buff_timeOver;

        kipTimeOver = timeOver;

        StartCoroutine("CoTimer");
    }

    public IEnumerator CoTimer()
    {
        while(true)
        {
            //yield return new WaitForSeconds(0.3f);

            timer += Time.deltaTime;

            if (timer > timeOver + 0.1f)
            {
                timer = 0;
                hero.Hit();
                WaitTimeReset();
                GameManager.inst.uiCombotex.ComboEnd();
            }

            //if(timer > 0.3f)
            //{
            //    slider.value = timer / timeOver;
            //}

            slider.value = timer / timeOver;

            yield return null;
        }
    }

    public void TimerReset()
    {
        timer = 0;
        StopCoroutine("CoTimer");
        StartCoroutine("CoTimer");
    }

    public void TimerStop()
    {
        slider.value = 1;
        StopCoroutine("CoTimer");
    }

    public void WaitTimeDown()
    {
        if (timeOver > 0.7f + Stat.buff_timeOver)
        {
            timeOver -= 0.1f;
        }
    }

    public void WaitTimeReset()
    {
        timeOver = kipTimeOver;
    }
    
    public float GetTimerSliderValue()
    {
        return slider.value;
    }
}
