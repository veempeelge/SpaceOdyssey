using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yodo1.MAS;

public class BannerAD : MonoBehaviour
{

    Yodo1U3dBannerAdView bannerAdView = null;
    private void Start()
    {
        bannerAdView = new Yodo1U3dBannerAdView(Yodo1U3dBannerAdSize.Banner, Yodo1U3dBannerAdPosition.BannerBottom | Yodo1U3dBannerAdPosition.BannerHorizontalCenter);
        SetupEventCallbacks();
        LoadBannerAd();
    }

    private void SetupEventCallbacks()
    {
        bannerAdView.OnAdLoadedEvent += OnBannerAdLoadedEvent;
        bannerAdView.OnAdFailedToLoadEvent += OnBannerAdLoadFailedEvent;
        bannerAdView.OnAdOpenedEvent += OnBannerAdOpenedEvent;
        bannerAdView.OnAdFailedToOpenEvent += OnBannerAdOpenFailedEvent;
        bannerAdView.OnAdClosedEvent += OnBannerAdClosedEvent;
    }

    private void LoadBannerAd()
    {
        bannerAdView.SetAdPlacement("Your placement id");
        bannerAdView.LoadAd();
    }

    private void OnBannerAdLoadedEvent(Yodo1U3dBannerAdView ad)
    {
        bannerAdView.Show();
    }

    private void OnBannerAdLoadFailedEvent(Yodo1U3dBannerAdView ad, Yodo1U3dAdError adError)
    {
        // Code to be executed when an ad request fails.
    }

    private void OnBannerAdOpenedEvent(Yodo1U3dBannerAdView ad)
    {
        // Code to be executed when an ad opened
    }

    private void OnBannerAdOpenFailedEvent(Yodo1U3dBannerAdView ad, Yodo1U3dAdError adError)
    {
        // Code to be executed when an ad open fails.
    }

    private void OnBannerAdClosedEvent(Yodo1U3dBannerAdView ad)
    {
        // Code to be executed when the ad closed
    }
}
