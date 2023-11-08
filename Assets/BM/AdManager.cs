using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AdManager : SingletonMonoBehaviour<AdManager>
{

    private Action m_RewardedAdComplete;
    private Action m_RewardedAdFail;
    
    private AdmobModule admobModule;


    private void Start()
    {
        admobModule = gameObject.AddComponent<AdmobModule>();
        admobModule.Initialize();
    }

    public bool TryShowRequest(Action rewardedAdComplete, Action rewardedAdFail = null)
    {
#if UNITRY_EDITOR
            m_RewardedAdComplete = rewardedAdComplete;
            RewardedAdCompletedHandler();
             return true;
#endif

        if (!admobModule.IsEnableShowAD())
        {
            //   UI_Popup_OK.Instance.Open("광고 로드 실패", "실행 가능한 광고가 없습니다. 잠시후 다시 시도해주세요.");
        
            return false;
        }
        
        m_RewardedAdComplete = rewardedAdComplete;
        m_RewardedAdFail = rewardedAdFail;
        
        admobModule.TryShowRewardAd(RewardedAdCompletedHandler, RewardedAdSkippedHandler);

        return true;
    }

    void RewardedAdCompletedHandler()
    {
        m_RewardedAdComplete?.Invoke();
        m_RewardedAdComplete = null;
    }


    void RewardedAdSkippedHandler()
    {
        m_RewardedAdFail?.Invoke();
        m_RewardedAdFail = null;
    }
}