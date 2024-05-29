using System.Collections.Generic;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using Yodo1.MAS;
using UnityEditor;
using System.IO;
using System.Net;
using System;
using System.Text;

public class Yodo1IdSync : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }
#if UNITY_ANDROID
    private String AdmobDefaultIDAndroid = "ca-app-pub-5580537606944457~4465578836";
#elif UNITY_IOS
    private String AdmobDefaultIDiOS = "ca-app-pub-5580537606944457~2166718551";
#endif
    public void OnPreprocessBuild(BuildReport report)
    {
#if UNITY_ANDROID
        if (!Yodo1AdUtils.IsGooglePlayVersion())
        {
            return;
        }
#endif

        Yodo1AdSettings settings = Yodo1AdSettingsSave.Load();

        string bundleId = string.Empty;
        string appKey = string.Empty;
#if UNITY_ANDROID
        appKey = settings.androidSettings.AppKey;
#elif UNITY_IOS
        appKey = settings.iOSSettings.AppKey;
#endif
        Dictionary<string, object> obj = Yodo1Net.GetInstance().GetAppInfoByAppKey(appKey);
        if (obj.ContainsKey("bundle_id"))
        {
            if (string.IsNullOrEmpty((string)obj["bundle_id"]))
            {
                UnityEngine.Debug.Log(Yodo1U3dMas.TAG + " Update the store linkwhen your game is live on Play Store or App Store.");
                return;
            }

        }
#if UNITY_ANDROID
        if (!string.Equals(settings.androidSettings.AdmobAppID, AdmobDefaultIDAndroid))
        {
            bundleId = PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.Android);
            Dictionary<string, object> androidData = Yodo1Net.GetInstance().GetAppInfoByBundleID("android", bundleId);
            Yodo1AdAssetsImporter.UpdateData(settings, androidData);
        }
#elif UNITY_IOS
        if (!string.Equals(settings.iOSSettings.AdmobAppID, AdmobDefaultIDiOS))
        {
            bundleId = PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.iOS);
            Dictionary<string, object> iosData = Yodo1Net.GetInstance().GetAppInfoByBundleID("iOS", bundleId);
            Yodo1AdAssetsImporter.UpdateData(settings, iosData);
        }
#endif
    }

}
