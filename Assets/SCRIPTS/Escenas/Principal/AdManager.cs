using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
//using admob;

public class AdManager : MonoBehaviour
{
    //script para q funcione la publicidad 
    public static AdManager Instance { set; get; }
    public static AdManager esteObjeto = null;
    //public string bannerId;
    //public string videoId;

    public BannerView bannerView;
    public InterstitialAd interstitial;
    public RewardBasedVideoAd rewardBasedVideo;

    public void Start()
    {
        if (esteObjeto == null) { esteObjeto = this; } else if (esteObjeto != this) { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);
        RequestBanner();
        RequestInterstitial();
        RequestRewardBasedVideo();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-1598053448886416/7959712892";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    public void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-1598053448886416/7127302011";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    public void mostrarInstersticial()
    {
        if (PlayerPrefs.GetInt("mostrarPublicidad") == 1)
        {
            if (interstitial.IsLoaded())
            {
                interstitial.Show();
            }
            RequestInterstitial();
        }
    }

    private void RequestRewardBasedVideo()
    {
#if UNITY_ANDROID
        string appId = "ca-app-pub-1598053448886416~7618735531";
#elif UNITY_IPHONE
            string appId = "ca-app-pub-1598053448886416~7618735531";
#else
            string appId = "unexpected_platform";
#endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        cargarRewardedVideo();
    }

    private void cargarRewardedVideo()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-1598053448886416/7964044029";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }
    
    public void mostrarRewardedVideo()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
        cargarRewardedVideo();
    }

    #region old method
    //private void Start()
    //{
    //    Instance = this;
    //    DontDestroyOnLoad(gameObject);
    //    Admob.Instance().initAdmob(bannerId, videoId);
    //    Admob.Instance().loadInterstitial();//CARGA EL VIDEO EN LA RAM
    //}

    //// mostrarBanner = muestra el banner arriba en el centro
    //public void mostrarBanner()
    //{
    //     Admob.Instance().showBannerRelative(AdSize.SmartBanner, AdPosition.TOP_CENTER, 5);
    //    //Admob.Instance().showBannerRelative(AdSize.Banner, AdPosition.BOTTOM_CENTER, 5);
    //}

    //public void mostrarVideo()
    //{
    //    if (Admob.Instance().isInterstitialReady())
    //    {
    //        Admob.Instance().showInterstitial();
    //    }
    //}
    #endregion
}
