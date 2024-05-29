using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yodo1.MAS;

public class Yodo1Manager : MonoBehaviour
{

    void Awake()
    {
        Yodo1U3dMas.InitializeMasSdk();
    }
    // Start is called before the first frame update
    void Start()
    {
        Yodo1U3dMasCallback.OnSdkInitializedEvent += (success, error) =>
        {
            Debug.Log("[Yodo1 Mas] OnSdkInitializedEvent, success:" + success + ", error: " + error.ToString());
            if (success)
            {
                Debug.Log("[Yodo1 Mas] The initialization has succeeded");
            }
            else
            {
                Debug.Log("[Yodo1 Mas] The initialization has failed");
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
