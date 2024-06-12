using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yodo1.MAS;
using System;
using UnityEngine.UI;
using TMPro;

public class Ads : MonoBehaviour
{
    [SerializeField] Button getRewardButton;
    private int retryAttempt = 0;
    private Yodo1U3dRewardAd _yodoReward;
    [SerializeField] GameObject rewardObtained;
    string cooldown;
    int cooldownNum;
    [SerializeField] TMP_Text cooldownTimer;

    private void Start()
    {
        Yodo1MasUserPrivacyConfig userPrivacyConfig = new Yodo1MasUserPrivacyConfig()
            .titleBackgroundColor(Color.green)
            .titleTextColor(Color.blue)
            .contentBackgroundColor(Color.black)
            .contentTextColor(Color.white)
            .buttonBackgroundColor(Color.red)
            .buttonTextColor(Color.green);

        Yodo1AdBuildConfig config = new Yodo1AdBuildConfig()
            .enableUserPrivacyDialog(true)
            .userPrivacyConfig(userPrivacyConfig)
            .enableATTAuthorization(true);
        Yodo1U3dMas.SetAdBuildConfig(config);
        Yodo1U3dMas.InitializeMasSdk();

        _yodoReward = Yodo1U3dRewardAd.GetInstance();

        getRewardButton.onClick.AddListener(LoadRewardAd);

        SetupEventCallbacks();
        _yodoReward.LoadAd();
        CheckCooldown();
    }

    private void SetupEventCallbacks()
    {
        _yodoReward.OnAdLoadFailedEvent += OnRewardAdLoadFailedEvent;
        _yodoReward.OnAdOpenedEvent += OnRewardAdOpenedEvent;
        _yodoReward.OnAdOpenFailedEvent += OnRewardAdOpenFailedEvent;
        _yodoReward.OnAdClosedEvent += OnRewardAdClosedEvent;
        _yodoReward.OnAdEarnedEvent += OnRewardAdEarnedEvent;
    }

    private void LoadRewardAd()
    {
        Debug.Log("ad");

        if (_yodoReward.IsLoaded())
            _yodoReward.ShowAd();
        else
        {
            _yodoReward.OnAdLoadedEvent += OnRewardAdLoadedEvent;
            _yodoReward.LoadAd();
        }
    }

    private void OnRewardAdLoadedEvent(Yodo1U3dRewardAd ad)
    {
        Yodo1U3dRewardAd.GetInstance().ShowAd();
        _yodoReward.OnAdLoadedEvent -= OnRewardAdLoadedEvent;
    }

    private void OnRewardAdLoadFailedEvent(Yodo1U3dRewardAd ad, Yodo1U3dAdError adError)
    {
        // Code to be executed when an ad request fails.
    }

    private void OnRewardAdOpenedEvent(Yodo1U3dRewardAd ad)
    {
        // Code to be executed when an ad opened
    }

    private void OnRewardAdOpenFailedEvent(Yodo1U3dRewardAd ad, Yodo1U3dAdError adError)
    {
        // Code to be executed when an ad open fails.
    }

    private void OnRewardAdClosedEvent(Yodo1U3dRewardAd ad)
    {
        Debug.Log("User closed ad, cancel reward");
    }

    private void OnRewardAdEarnedEvent(Yodo1U3dRewardAd ad)
    {
        var nextAdTime = DateTime.Now.AddMinutes(0.3f);
        Cooldown(nextAdTime);
        getRewardButton.interactable = false;
        rewardObtained.SetActive(true);
        Debug.Log("Reward earned");
        CheckCooldown();
        TimeManager.instance.EnergyReward();
    }

    private void Cooldown(DateTime adCooldown)
    {
        var dateTimeString = adCooldown.ToString();
        PlayerPrefs.SetString("RewardCooldown", dateTimeString);
        StartCoroutine(UpdateCooldownTimer(adCooldown));
    }

    private void CheckCooldown()
    {
        var parsedDateTime = DateTime.Parse(PlayerPrefs.GetString("RewardCooldown", DateTime.Now.ToString()));
        var timeLeft = (parsedDateTime - DateTime.Now).TotalSeconds;

        Debug.Log("Cooldown = " + timeLeft);
        if (timeLeft <= 0)
        {
            getRewardButton.interactable = true;
        }
        else
        {
            getRewardButton.interactable = false;
            StartCoroutine(DelayAdButton((int)timeLeft));
            cooldownNum = (int)timeLeft;
            Debug.Log(timeLeft);
            StartCoroutine(UpdateCooldownTimer(parsedDateTime));
        }
    }

    private IEnumerator DelayAdButton(int timeLeft)
    {
        yield return new WaitForSeconds(timeLeft);
        getRewardButton.interactable = true;
    }

    private IEnumerator UpdateCooldownTimer(DateTime endTime)
    {
        while (true)
        {
            var timeLeft = endTime - DateTime.Now;
            if (timeLeft.TotalSeconds <= 0)
            {
                cooldownTimer.SetText("00:00:00");
                yield break;
            }

            cooldownTimer.SetText(timeLeft.ToString(@"hh\:mm\:ss"));
            yield return new WaitForSeconds(1);
        }
    }
}
