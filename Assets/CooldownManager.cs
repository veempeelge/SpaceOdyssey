using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CooldownManager : MonoBehaviour
{
    public static CooldownManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartCooldownTimer(DateTime endTime, TMP_Text cooldownTimer, Button getRewardButton)
    {
        StartCoroutine(UpdateCooldownTimer(endTime, cooldownTimer, getRewardButton));
    }

    private IEnumerator UpdateCooldownTimer(DateTime endTime, TMP_Text cooldownTimer, Button getRewardButton)
    {
        while (true)
        {
            var timeLeft = endTime - DateTime.Now;
            if (timeLeft.TotalSeconds <= 0)
            {
                if (cooldownTimer != null)
                {
                    cooldownTimer.SetText("00:00:00");
                }
                getRewardButton.interactable = true;
                yield break;
            }

            if (cooldownTimer != null)
            {
                cooldownTimer.SetText(timeLeft.ToString(@"hh\:mm\:ss"));
            }

            yield return new WaitForSeconds(1);
        }
    }
}

