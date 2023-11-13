using System;
using GoogleMobileAds.Api;
using UnityEngine;


public class AdmobModule : MonoBehaviour
{
    private RewardedAd _rewardedAd;
    private InterstitialAd _interstitialAd;
    
    private string rewardADUnitID = "ca-app-pub-1076272347919893/7079684757";
    private string interstiatialID = "ca-app-pub-1076272347919893/8738297074";

    private Action _onSuccess;
    private Action _onFail;

    private bool _isRewarded;
    private bool _isComplete;

    public void Initialize()
    {
        // 광고-유니티 스레드 충돌 해결
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize(initStatus => { Debug.LogError("애드몹 초기화 완료"); });  

        LoadRewardedAd();
        LoadInterstitialAd();

        RegisterReloadHandler(_interstitialAd);
    }

    private void Update()
    {
        if (_isComplete)
        {
            if (!_isRewarded)
            {
                _onFail?.Invoke();
            }
            else
            {
                _onSuccess?.Invoke();
            }

            _isComplete = false;
            _isRewarded = false;
            _onSuccess = null;
            _onFail = null;
        }
    }

    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        RewardedAd.Load(rewardADUnitID, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {                
                    Debug.Log("Fail Loading AD.");
                    return;
                }

                _rewardedAd = ad;
                Debug.Log("Success Loading AD.");
                
                RegisterReloadHandler(ad);
                RegisterEventHandlers(ad);
            });
    }

    private void RegisterReloadHandler(RewardedAd ad)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            _isComplete = true;
            LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            LoadRewardedAd();
        };
    }
    
    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

    public bool IsEnableShowAD()
    {
        bool isLoaded = _rewardedAd == null || _rewardedAd.CanShowAd();

        if (!isLoaded)
        {
            LoadRewardedAd();
        }

        return isLoaded;
    }

    public void TryShowRewardAd(Action onSuccessCallback, Action onSkippedCallback)
    {
        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _onSuccess = onSuccessCallback;
            _onFail = onSkippedCallback;
            
            _rewardedAd.Show((GoogleMobileAds.Api.Reward reward) =>
            {
                _isRewarded = true;
            });
        }
    }
    
    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(interstiatialID, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                _interstitialAd = ad;
            });
    }
    public void ShowInterstitialAd()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            _interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
            LoadInterstitialAd();
        }
    }
    
    private void RegisterReloadHandler(InterstitialAd interstitialAd)
    {
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial Ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitialAd();
        };
        
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitialAd();
        };
    }
}