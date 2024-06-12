using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlanesCount : MonoBehaviour
{
    public static PlanesCount Instance;

    public int planesCount;
    int maxPlanes = 5; // Assuming a maximum planes count for demonstration
    public int replenishTimerMinutes = 4;

    private const string ReplenishPrefsKey = "ReplenishCooldown";
    private const string LastExitTimePrefsKey = "LastExitTime";
    private const string PlaneCountSave = "PlaneCountSave";

    [SerializeField] TMP_Text planesText;
    [SerializeField] TMP_Text cooldownTimerText;

    // Start is called before the first frame update
    void Start()
    {
        planesCount = PlayerPrefs.GetInt(PlaneCountSave, maxPlanes);
        Instance = this;
        CheckOfflineReplenish();
        PlaneReplenish();
        UpdatePlanesText();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString(LastExitTimePrefsKey, DateTime.Now.ToString());
        PlayerPrefs.Save();
    }

    public void UsedPlane()
    {
        if (planesCount > 0)
        {
            planesCount--;
            UpdatePlanesText();
            if (planesCount < maxPlanes)
            {
                var nextPlaneReplenish = DateTime.Now.AddMinutes(replenishTimerMinutes);
                PlayerPrefs.SetString(ReplenishPrefsKey, nextPlaneReplenish.ToString());
                PlayerPrefs.Save();
                StartCoroutine(ReplenishTimer(nextPlaneReplenish));
            }
        }
    }

    public void GotPlane()
    {
        if (planesCount >= maxPlanes) return;
        planesCount++;
        UpdatePlanesText();
        if (planesCount < maxPlanes)
        {
            var nextPlaneReplenish = DateTime.Now.AddMinutes(replenishTimerMinutes);
            PlayerPrefs.SetString(ReplenishPrefsKey, nextPlaneReplenish.ToString());
            PlayerPrefs.Save();
            StartCoroutine(ReplenishTimer(nextPlaneReplenish));
        }
    }

    public void PlaneReplenish()
    {
        if (PlayerPrefs.HasKey(ReplenishPrefsKey))
        {
            var parsedDateTime = DateTime.Parse(PlayerPrefs.GetString(ReplenishPrefsKey));
            if (parsedDateTime > DateTime.Now)
            {
                StartCoroutine(ReplenishTimer(parsedDateTime));
            }
        }
    }

    private void CheckOfflineReplenish()
    {
        if (PlayerPrefs.HasKey(LastExitTimePrefsKey))
        {
            var lastExitTime = DateTime.Parse(PlayerPrefs.GetString(LastExitTimePrefsKey));
            var timeAway = DateTime.Now - lastExitTime;
            int planesToAdd = Mathf.FloorToInt((float)timeAway.TotalMinutes / replenishTimerMinutes);

            planesCount = Mathf.Clamp(planesCount + planesToAdd, 0, maxPlanes);
            UpdatePlanesText();

            if (planesCount < maxPlanes)
            {
                var remainingTimeForNextPlane = replenishTimerMinutes - (timeAway.TotalMinutes % replenishTimerMinutes);
                var nextReplenishTime = DateTime.Now.AddMinutes(remainingTimeForNextPlane);
                PlayerPrefs.SetString(ReplenishPrefsKey, nextReplenishTime.ToString());
                PlayerPrefs.Save();
                StartCoroutine(ReplenishTimer(nextReplenishTime));
            }
        }
    }

    private IEnumerator ReplenishTimer(DateTime nextReplenishTime)
    {
        while (true)
        {
            var timeRem = nextReplenishTime - DateTime.Now;
            if (timeRem.TotalSeconds <= 0)
            {
                cooldownTimerText.SetText("00:00:00");
                GotPlane();
                yield break;
            }

            cooldownTimerText.SetText(timeRem.ToString(@"hh\:mm\:ss"));
            yield return new WaitForSeconds(1);
        }
    }

    private void UpdatePlanesText()
    {
        planesText.SetText($"Planes: {planesCount}/{maxPlanes}");
        PlayerPrefs.SetInt(PlaneCountSave, planesCount);
    }
}
