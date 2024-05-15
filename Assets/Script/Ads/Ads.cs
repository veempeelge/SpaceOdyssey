using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yodo1.MAS;
using System;

public class Ads : MonoBehaviour
{
    private int retryAttempt = 0;

    private void Start()
    {
        SetupEventCallbacks();
        LoadRewardAd();
    }

    private void SetupEventCallbacks()
    {
        Yodo1U3dRewardAd.GetInstance().OnAdLoadedEvent += OnRewardAdLoadedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdLoadFailedEvent += OnRewardAdLoadFailedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdOpenedEvent += OnRewardAdOpenedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdOpenFailedEvent += OnRewardAdOpenFailedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdClosedEvent += OnRewardAdClosedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdEarnedEvent += OnRewardAdEarnedEvent;
    }

    private void LoadRewardAd()
    {
        Yodo1U3dRewardAd.GetInstance().LoadAd();
    }

    private void OnRewardAdLoadedEvent(Yodo1U3dRewardAd ad)
    {
        // Code to be executed when an ad finishes loading.
        retryAttempt = 0;
        Yodo1U3dRewardAd.GetInstance().ShowAd();
    }

    private void OnRewardAdLoadFailedEvent(Yodo1U3dRewardAd ad, Yodo1U3dAdError adError)
    {
        // Code to be executed when an ad request fails.
        retryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt));
        Invoke("LoadRewardAd", (float)retryDelay);
    }

    private void OnRewardAdOpenedEvent(Yodo1U3dRewardAd ad)
    {
        // Code to be executed when an ad opened
    }

    private void OnRewardAdOpenFailedEvent(Yodo1U3dRewardAd ad, Yodo1U3dAdError adError)
    {
        // Code to be executed when an ad open fails.
        LoadRewardAd();
    }

    private void OnRewardAdClosedEvent(Yodo1U3dRewardAd ad)
    {
        // Code to be executed when the ad closed
        LoadRewardAd();
    }

    private void OnRewardAdEarnedEvent(Yodo1U3dRewardAd ad)
    {
        // Code executed when getting rewards
    }
}

