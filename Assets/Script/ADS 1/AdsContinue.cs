using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yodo1.MAS;
using System;
using UnityEngine.UI;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;

public class AdsContinue : MonoBehaviour
{
    [SerializeField] Button getRewardButton;
    private int retryAttempt = 0;
    private Yodo1U3dRewardAd _yodoReward;
    private int scoreToRestart;
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
        Cooldown();
        getRewardButton.interactable = false;
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
        

        Debug.Log("Reward earned");
        SceneManager.LoadScene("Game-1");
    }
    
    private void Cooldown()
    {
        var nextAddTime = DateTime.Now.AddMinutes(30);

        var dateTimeString = nextAddTime.ToString();

        if (DateTime.TryParse(dateTimeString, out var parsedDateTime))
        {
            var timeLeft = (parsedDateTime - DateTime.Now).TotalSeconds;

            if (timeLeft <= 0)
            {

            }
            else
            {
                StartCoroutine(DelayAdButton((int)timeLeft));
            }
        }
    }

    private IEnumerator DelayAdButton(int timeLeft)
    {
        yield return new WaitForSeconds(timeLeft);
        getRewardButton.interactable = true;
    }
}

