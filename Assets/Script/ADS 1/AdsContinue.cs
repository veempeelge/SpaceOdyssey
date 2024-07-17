using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yodo1.MAS;
using System;
using UnityEngine.UI;
//using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;
using TMPro;

public class AdsContinue : MonoBehaviour
{
    [SerializeField] Button getRewardButton;
    private int retryAttempt = 0;
    private Yodo1U3dRewardAd _yodoReward;
    private int scoreToRestart;
    string cooldown;
    int cooldownNum;
    [SerializeField] TMP_Text cooldownTimerContinue;
   // [SerializeField] GameObject rewardObtained;

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
    //    cooldownTimerContinue.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        RemoveEventCallbacks();
    }

    private void SetupEventCallbacks()
    {
        _yodoReward.OnAdLoadFailedEvent += OnRewardAdLoadFailedEvent;
        _yodoReward.OnAdOpenedEvent += OnRewardAdOpenedEvent;
        _yodoReward.OnAdOpenFailedEvent += OnRewardAdOpenFailedEvent;
        _yodoReward.OnAdClosedEvent += OnRewardAdClosedEvent;
        _yodoReward.OnAdEarnedEvent += OnRewardAdEarnedEvent;
    }

    private void RemoveEventCallbacks()
    {
        _yodoReward.OnAdLoadFailedEvent -= OnRewardAdLoadFailedEvent;
        _yodoReward.OnAdOpenedEvent -= OnRewardAdOpenedEvent;
        _yodoReward.OnAdOpenFailedEvent -= OnRewardAdOpenFailedEvent;
        _yodoReward.OnAdClosedEvent -= OnRewardAdClosedEvent;
        _yodoReward.OnAdEarnedEvent -= OnRewardAdEarnedEvent;
    }
    private void OnRewardAdLoadedEvent(Yodo1U3dRewardAd ad)
    {
        // Code to be executed when an ad finishes loading.
        //retryAttempt = 0;
        Yodo1U3dRewardAd.GetInstance().ShowAd();
        _yodoReward.OnAdLoadedEvent -= OnRewardAdLoadedEvent;
    }

    private void OnRewardAdLoadFailedEvent(Yodo1U3dRewardAd ad, Yodo1U3dAdError adError)
    {
        // Code to be executed when an ad request fails.
        //retryAttempt++;
        //double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt));
        //Invoke("LoadRewardAd", (float)retryDelay);
    }

    private void OnRewardAdOpenedEvent(Yodo1U3dRewardAd ad)
    {
        // Code to be executed when an ad opened
    }

    private void OnRewardAdOpenFailedEvent(Yodo1U3dRewardAd ad, Yodo1U3dAdError adError)
    {
        // Code to be executed when an ad open fails.
        //_yodoReward.LoadAd();
    }

    private void OnRewardAdClosedEvent(Yodo1U3dRewardAd ad)
    {
        //// Code to be executed when the ad closed
        ////_yodoReward.LoadAd();
        //Debug.Log("User closed ad, cancel reward");
        //scoreToRestart = 0;
        //PlayerPrefs.SetInt("StartScore", 0);
    }

    private void OnRewardAdEarnedEvent(Yodo1U3dRewardAd ad)
    {
        // Code executed when getting rewards
        scoreToRestart =  PlayerPrefs.GetInt("EndScore");
        PlayerPrefs.SetInt("StartScore", scoreToRestart);
        var nextAdTime = DateTime.Now.AddMinutes(30f);
        Cooldown(nextAdTime);
        Debug.Log("Reward earned");

        SceneManager.LoadScene("Game-1");
        cooldownTimerContinue.gameObject.SetActive(true);
        CheckCooldown();
    }
    
 
    private IEnumerator DelayAdButton(int timeLeft)
    {
        yield return new WaitForSeconds(timeLeft);
        getRewardButton.interactable = true;
    }

    private void Cooldown(DateTime adCooldown)
    {
        var dateTimeString = adCooldown.ToString();
        PlayerPrefs.SetString("RewardCooldownContinue", dateTimeString);
        if(this != null)
        StartCoroutine(UpdateCooldownTimer(adCooldown));
        else
            return;
        Debug.Log("ah");
    }

    private void CheckCooldown()
    {
        var parsedDateTime = DateTime.Parse(PlayerPrefs.GetString("RewardCooldownContinue", DateTime.Now.ToString()));
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
            cooldownTimerContinue.gameObject.SetActive(true);

        }
    }

    private IEnumerator UpdateCooldownTimer(DateTime endTime)
    {
        while (true)
        {
            var timeLeft = endTime - DateTime.Now;
            if (timeLeft.TotalSeconds <= 0)
            {
                cooldownTimerContinue.SetText("00:00:00");
                cooldownTimerContinue.gameObject.SetActive(false);
                yield break;
            }

            cooldownTimerContinue.SetText(timeLeft.ToString(@"hh\:mm\:ss"));
            yield return new WaitForSeconds(1);
        }
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
}

