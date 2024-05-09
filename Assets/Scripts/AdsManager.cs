using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
#if UNITY_IOS
    string gameId = "5606942";
#else 
    string gameId = "5606943";
#endif


    void Start()
    {
        Advertisement.Initialize(gameId);
    }

    public void AdPlay()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Show("video");
        }
    }
}
