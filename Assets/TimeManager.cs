using System.Collections;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public int maxEnergy = 5;
    public int energy;
    public float timeToRecoverEnergy;

    DateTime lastEnergySpendDateTime; // time/date when energy was last spent
    float timePassed;

    UpdateText updateTXT;
    public TMP_Text timerText; // Add this for the timer text


    private void Awake()
    {
        instance = this;
        energy = PlayerPrefs.GetInt("energy", maxEnergy);
    }

    // Start is called before the first frame update
    void Start()
    {
        updateTXT = FindObjectOfType<UpdateText>();
        LoadDate();
        StartCoroutine(EnergyRecoveryCoroutine());
        updateTXT.UpdateTextValue(energy);
    }

    [Obsolete]
    public void SpendEnergy()
    {
        //energy = PlayerPrefs.GetInt("energy", maxEnergy);
        if (energy > 0)
        {
            energy--;
            StartGame();
            // ParticleTransition.instance.StartGame();

            if (energy < maxEnergy)
            {
                if (lastEnergySpendDateTime == DateTime.MinValue)
                {
                    lastEnergySpendDateTime = DateTime.Now; // Only reset if not already set
                }
               
            }
            SaveData();
            // TODO: PLAY GAME
        }
        else
        {
            Debug.Log("Not enough energy to spend");
        }

        
    }

    void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void RecoverEnergyWithTime()
    {
        if (lastEnergySpendDateTime == DateTime.MinValue)
            return;

        timePassed = (float)(DateTime.Now - lastEnergySpendDateTime).TotalSeconds;

        if (timePassed >= timeToRecoverEnergy)
        {
            int energyToRecover = Mathf.FloorToInt(timePassed / timeToRecoverEnergy);
            energy += energyToRecover;
            lastEnergySpendDateTime = DateTime.Now;

            if (energy >= maxEnergy)
            {
                energy = maxEnergy;
                lastEnergySpendDateTime = DateTime.MinValue; // Reset to initial state
            }
            else
            {
                lastEnergySpendDateTime = DateTime.Now.AddSeconds(-(timePassed % timeToRecoverEnergy)); // Keep track of remaining time for next recovery
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
            float remainingTime = timeToRecoverEnergy - (float)(DateTime.Now - lastEnergySpendDateTime).TotalSeconds;
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
        timePassed = (float)(DateTime.Now - lastEnergySpendDateTime).TotalSeconds;

        if (timePassed >= timeToRecoverEnergy)
        {
            int energyToRecover = Mathf.FloorToInt(timePassed / timeToRecoverEnergy);
            energy += energyToRecover;

            if (energy >= maxEnergy)
            {
                energy = maxEnergy;
                lastEnergySpendDateTime = DateTime.MinValue; // Reset to initial state
            }
            else
            {
                lastEnergySpendDateTime = DateTime.Now.AddSeconds(-(timePassed % timeToRecoverEnergy)); // Keep track of remaining time for next recovery
            }
        }
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("energy", energy);
        PlayerPrefs.SetString("lastEnergySpendDateTime", lastEnergySpendDateTime.ToString());
    }

    public void LoadDate()
    {
        if (PlayerPrefs.HasKey("lastEnergySpendDateTime"))
        {
            lastEnergySpendDateTime = Convert.ToDateTime(PlayerPrefs.GetString("lastEnergySpendDateTime"));
            energy = PlayerPrefs.GetInt("energy");
            RecoverEnergyOnGameLoad();
        }
        else
        {
            energy = maxEnergy;
            lastEnergySpendDateTime = DateTime.MinValue; // Initial state
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
        if (energy < maxEnergy)
        {
            energy++;
            if (energy == maxEnergy)
            {
                lastEnergySpendDateTime = DateTime.MinValue;
            }
            SaveData();
         
        }
    }
}
