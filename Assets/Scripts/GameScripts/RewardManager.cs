using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public YandexSDK sdk;
    // Start is called before the first frame update
    void Start()
    {
        sdk = YandexSDK.instance;
        sdk.onInterstitialShown += SDKNull;
        sdk.onInterstitialFailed += SDKNull;
    }
    void SDKNull(string s)
    {

    }
    void SDKNull()
    {
        
    }
    public void AddShow()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        sdk.ShowInterstitial();
#endif
    }
    public void TimeSet(float time)
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        sdk.SaveToLeaderBoard(time);
#endif
    }
}
