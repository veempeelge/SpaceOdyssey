using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yodo1.MAS;


public class Ads : MonoBehaviour
{
    [SerializeField] private Button getRewardButton;
    private Yodo1U3dRewardAd _yodoReward;
    [SerializeField] private GameObject rewardObtained;
    [SerializeField] private TMP_Text cooldownTimer;

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
        Debug.Log("Ad is loading");

        if (_yodoReward.IsLoaded())
        {
            _yodoReward.ShowAd();
        }
        else
        {
            _yodoReward.OnAdLoadedEvent += OnRewardAdLoadedEvent;
            _yodoReward.LoadAd();
        }
    }

    private void OnRewardAdLoadedEvent(Yodo1U3dRewardAd ad)
    {
        _yodoReward.OnAdLoadedEvent -= OnRewardAdLoadedEvent;
        _yodoReward.ShowAd();
    }

    private void OnRewardAdLoadFailedEvent(Yodo1U3dRewardAd ad, Yodo1U3dAdError adError)
    {
        Debug.LogError($"Ad load failed: {adError}");
    }

    private void OnRewardAdOpenedEvent(Yodo1U3dRewardAd ad)
    {
        Debug.Log("Ad opened");
    }

    private void OnRewardAdOpenFailedEvent(Yodo1U3dRewardAd ad, Yodo1U3dAdError adError)
    {
        Debug.LogError($"Ad open failed: {adError}");
    }

    private void OnRewardAdClosedEvent(Yodo1U3dRewardAd ad)
    {
        Debug.Log("Ad closed without reward");
    }

    private void OnRewardAdEarnedEvent(Yodo1U3dRewardAd ad)
    {
        var nextAdTime = DateTime.Now.AddMinutes(2);
        Cooldown(nextAdTime);
        getRewardButton.interactable = false;
        rewardObtained.SetActive(true);
        Debug.Log("Reward earned");
        cooldownTimer.gameObject.SetActive(true);
        CheckCooldown();
        TimeManager.instance.EnergyReward();
    }

    private void Cooldown(DateTime adCooldown)
    {
        PlayerPrefs.SetString("RewardCooldown", adCooldown.ToString());
        PlayerPrefs.Save();
        StartCoroutine(UpdateCooldownTimer(adCooldown));
    }

    private void CheckCooldown()
    {
        if (PlayerPrefs.HasKey("RewardCooldown"))
        {
            var parsedDateTime = DateTime.Parse(PlayerPrefs.GetString("RewardCooldown"));
            var timeLeft = (parsedDateTime - DateTime.Now).TotalSeconds;

            if (timeLeft <= 0)
            {
                getRewardButton.interactable = true;
            }
            else
            {
                getRewardButton.interactable = false;
                StartCoroutine(DelayAdButton((int)timeLeft));
                cooldownTimer.gameObject.SetActive(true);
                StartCoroutine(UpdateCooldownTimer(parsedDateTime));
            }
        }
        else
        {
            getRewardButton.interactable = true;
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
                cooldownTimer.gameObject.SetActive(false);
                yield break;
            }

            cooldownTimer.SetText(timeLeft.ToString(@"hh\:mm\:ss"));
            yield return new WaitForSeconds(1);
        }
    }
}
