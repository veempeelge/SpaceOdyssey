using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yodo1.MAS;

public class TriggerAd : MonoBehaviour
{
    [SerializeField] Button rewardButton;
    // Start is called before the first frame update
    void Start()
    {
        rewardButton.onClick.AddListener(ShowAd);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowAd()
    {
        Debug.Log("Ad");
        Yodo1U3dRewardAd.GetInstance().LoadAd();
    }
}
