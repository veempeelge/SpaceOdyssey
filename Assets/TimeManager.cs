using System.Collections;
using UnityEngine;
using System;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public int maxEnergy = 5;
    public int energy;
    public float timeToRecoverEnergy;

    DateTime energySpendDateTime; //time/date when energy was spent
    float timePassed;

    UpdateText updateTXT;
    public TMP_Text timerText; // Add this for the timer text

    private void Awake()
    {
        energy = PlayerPrefs.GetInt("energy", 5);
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
  
        updateTXT = FindObjectOfType<UpdateText>();
        LoadDate();
        StartCoroutine(EnergyRecoveryCoroutine());
        updateTXT.UpdateTextValue(energy);
    }

    private void Update()
    {
        //Debug.Log(energy);
    }

    public void SpendEnergy()
    {
        if (energy > 0)
        {
            energy--;
            energySpendDateTime = System.DateTime.Now;
            SaveData();
            //TODO: PLAY GAME
        }
        else
        {
            Debug.Log("Not enough energy to spend");
        }
    }

    public void RecoverEnergyWithTime()
    {
        timePassed = (float)(System.DateTime.Now - energySpendDateTime).TotalSeconds;

        if (timePassed >= timeToRecoverEnergy)
        {
            energy++;
            energySpendDateTime = System.DateTime.Now;

            if (energy >= maxEnergy)
            {
                energy = maxEnergy;
            }
        }
    }

    IEnumerator EnergyRecoveryCoroutine()
    {
        while (true)
        {
            RecoverEnergyWithTime();
            updateTXT.UpdateTextValue(energy);
            UpdateTimerText(); // Update the timer text
            yield return new WaitForSeconds(1);
        }
    }

    void UpdateTimerText()
    {
        if (energy < maxEnergy)
        {
            float remainingTime = timeToRecoverEnergy - (float)(System.DateTime.Now - energySpendDateTime).TotalSeconds;
            if (remainingTime < 0) remainingTime = 0;

            TimeSpan timeSpan = TimeSpan.FromSeconds(remainingTime);
            string timerString = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
            timerText.text = timerString;
        }
        else
        {
            timerText.text = "00:00";
        }
    }

    public void RecoverEnergyOnGameLoad()
    {
        timePassed = (float)(System.DateTime.Now - energySpendDateTime).TotalSeconds;

        if (timePassed >= timeToRecoverEnergy)
        {
            energy += Mathf.RoundToInt(timePassed / timeToRecoverEnergy);

            if (energy >= maxEnergy)
            {
                energy = maxEnergy;
            }
        }
        energySpendDateTime = System.DateTime.Now;
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("energy", energy);
        PlayerPrefs.SetString("energySpendDateTime", energySpendDateTime.ToString());
    }

    public void LoadDate()
    {
        if (PlayerPrefs.HasKey("energySpendDateTime"))
        {
            energySpendDateTime = Convert.ToDateTime(PlayerPrefs.GetString("energySpendDateTime"));
            energy = PlayerPrefs.GetInt("energy");
            RecoverEnergyOnGameLoad();
        }
        else
        {
            energy = maxEnergy;
            energySpendDateTime = System.DateTime.Now;
        }
        updateTXT.UpdateTextValue(energy);
        UpdateTimerText();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void EnergyReward()
    {
        energy++;
    }
}
