using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TapsellPlusSDK;

public class Ad_Manager : MonoBehaviour
{
    public static Ad_Manager instance;

    const string TAPSELL_KEY = "rkholcrojorksadnsftbnkiljtqlsnjgsthhecpgjmgqblqnmeeddebcgtpcidesaasego";
    const string VIDEO_ZONE_ID = "621efc38202a654f4ece717e";
    const string BANNER_ZONE_ID = "621f1a30aa049400fb8f25cc";
    //string responseID = "";


    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);

        TapsellPlus.Initialize(TAPSELL_KEY, adNetworkName => Debug.Log(adNetworkName + " Initialized Successfully."),
            error => Debug.Log(error.ToString()));
        TapsellPlus.SetGdprConsent(true);
    }

    public void RequestVideo()
    {
        TapsellPlus.RequestInterstitialAd(VIDEO_ZONE_ID, (tap_Ad_Module) =>
        {            
            ShowVideo(tap_Ad_Module.responseId);
        },
            (error) => { });
    }

    private void ShowVideo(string responseID)
    {
        TapsellPlus.ShowInterstitialAd(responseID, (tap_Ad_Module) => { },
            (tap_Ad_Module) => { }, (error) => { });        
    }    

    public void RequestBanner()
    {
        TapsellPlus.RequestInterstitialAd(BANNER_ZONE_ID, (tap_Ad_Module) =>       
        {
            ShowBanner(tap_Ad_Module.responseId); 
        }, (error) => { });
    }

    private void ShowBanner(string responseID)
    {
        TapsellPlus.ShowInterstitialAd(responseID, (tap_Ad_Module) => { },
            (tap_Ad_Module) => { }, (error) => { });
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
