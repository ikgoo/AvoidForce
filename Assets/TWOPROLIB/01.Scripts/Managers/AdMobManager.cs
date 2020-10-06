//using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 테스트 id : https://developers.google.com/admob/unity/test-ads?hl=ko#enable_test_devices
// 참고url : https://developers.google.com/admob/unity/start?hl=ko
// 전면광고 : https://developers.google.com/admob/unity/interstitial?hl=ko

public class AdMobManager : MonoBehaviour
{
    //    private BannerView bannerView;
    //    private InterstitialAd interstitial;
    //    private RewardedAd rewardedAd;
    //    private float deltaTime = 0.0f;
    //    private static string outputMessage = string.Empty;

    //    public static string OutputMessage
    //    {
    //        set { outputMessage = value; }
    //    }

    //    /// <summary>
    //    /// 안드로이드 앱 아이디
    //    /// </summary>
    //    [Tooltip("안드로이드 앱 아이디")]
    //    public string androidAppid = "ca-app-pub-3829564779469677~5251271262";
    //    /// <summary>
    //    /// 아이폰 앱 아이디
    //    /// </summary>
    //    [Tooltip("아이폰 광고 앱 아이디")]
    //    public string iphoneAppid;

    //    /// <summary>
    //    /// 광고 키원드[game, unity, study 등등]
    //    /// </summary>
    //    [Tooltip("광고 키원드[game, unity, study 등등]")]
    //    public List<string> keywords = new List<string>();

    //    /// <summary>
    //    /// 광고 디버깅 유무
    //    /// </summary>
    //    [Tooltip("광고 디버깅 테스트 박스")]
    //    public bool debugShowBox = false;

    //    [Tooltip("체크하면 테스트 아이디를 씀")]
    //    public bool testId = false;
    //    //테스트용
    //    public Text testText;
    //    public void Start()
    //    {

    //#if UNITY_ANDROID
    //        string appId = androidAppid;
    //#elif UNITY_IPHONE
    //        string appId = iphoneAppid;
    //#else
    //        string appId = "unexpected_platform";
    //#endif

    //        MobileAds.SetiOSAppPauseOnBackground(true);

    //        // Initialize the Google Mobile Ads SDK.
    //        MobileAds.Initialize(appId);

    //        this.RequestInterstitial();
    //    }

    //    public void Update()
    //    {
    //        if (debugShowBox == true)
    //        {
    //            // Calculate simple moving average for time to render screen. 0.1 factor used as smoothing
    //            // value.
    //            this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;

    //        }

    //    }

    //    public void OnGUI()
    //    {
    //        if (debugShowBox == true)
    //        {
    //            GUIStyle style = new GUIStyle();

    //            Rect rect = new Rect(0, 0, Screen.width, Screen.height);
    //            style.alignment = TextAnchor.LowerRight;
    //            style.fontSize = (int)(Screen.height * 0.06);
    //            style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
    //            float fps = 1.0f / this.deltaTime;
    //            string text = string.Format("{0:0.} fps", fps);
    //            GUI.Label(rect, text, style);

    //            // Puts some basic buttons onto the screen.
    //            GUI.skin.button.fontSize = (int)(0.035f * Screen.width);
    //            float buttonWidth = 0.35f * Screen.width;
    //            float buttonHeight = 0.15f * Screen.height;
    //            float columnOnePosition = 0.1f * Screen.width;
    //            float columnTwoPosition = 0.55f * Screen.width;

    //            Rect requestBannerRect = new Rect(
    //                columnOnePosition,
    //                0.05f * Screen.height,
    //                buttonWidth,
    //                buttonHeight);
    //            if (GUI.Button(requestBannerRect, "Request\nBanner"))
    //            {
    //                this.RequestBanner();
    //            }

    //            Rect destroyBannerRect = new Rect(
    //                columnOnePosition,
    //                0.225f * Screen.height,
    //                buttonWidth,
    //                buttonHeight);
    //            if (GUI.Button(destroyBannerRect, "Destroy\nBanner"))
    //            {
    //                this.bannerView.Destroy();
    //            }

    //            Rect requestInterstitialRect = new Rect(
    //                columnOnePosition,
    //                0.4f * Screen.height,
    //                buttonWidth,
    //                buttonHeight);
    //            if (GUI.Button(requestInterstitialRect, "Request\nInterstitial"))
    //            {
    //                this.RequestInterstitial();
    //            }

    //            Rect showInterstitialRect = new Rect(
    //                columnOnePosition,
    //                0.575f * Screen.height,
    //                buttonWidth,
    //                buttonHeight);
    //            if (GUI.Button(showInterstitialRect, "Show\nInterstitial"))
    //            {
    //                this.ShowInterstitial();
    //            }

    //            Rect destroyInterstitialRect = new Rect(
    //                columnOnePosition,
    //                0.75f * Screen.height,
    //                buttonWidth,
    //                buttonHeight);
    //            if (GUI.Button(destroyInterstitialRect, "Destroy\nInterstitial"))
    //            {
    //                this.interstitial.Destroy();
    //            }

    //            Rect requestRewardedRect = new Rect(
    //                columnTwoPosition,
    //                0.05f * Screen.height,
    //                buttonWidth,
    //                buttonHeight);
    //            if (GUI.Button(requestRewardedRect, "Request\nRewarded Ad"))
    //            {
    //                this.CreateAndLoadRewardedAd();
    //            }

    //            Rect showRewardedRect = new Rect(
    //                columnTwoPosition,
    //                0.225f * Screen.height,
    //                buttonWidth,
    //                buttonHeight);
    //            if (GUI.Button(showRewardedRect, "Show\nRewarded Ad"))
    //            {
    //                this.ShowRewardedAd();
    //            }

    //            //Rect textOutputRect = new Rect(
    //            //    columnTwoPosition,
    //            //    0.925f * Screen.height,
    //            //    buttonWidth,
    //            //    0.05f * Screen.height);
    //            //GUI.Label(textOutputRect, outputMessage);


    //        }

    //    }

    //    // Returns an ad request with custom ad targeting.
    //    private AdRequest CreateAdRequest()
    //    {
    //        AdRequest.Builder b = new AdRequest.Builder();

    //        // 디버깅 시 테스트 디바이스 사용
    //        //if (Debug.isDebugBuild)
    //        //{
    //        //    b.AddTestDevice(AdRequest.TestDeviceSimulator);
    //        //    b.AddTestDevice("0123456789ABCDEF0123456789ABCDEF");
    //        //}
    //        //for (int i = 0; i < keywords.Count; i++)
    //        //{
    //        //    b.AddKeyword(keywords[i]);
    //        //}
    //        ////b.SetGender(Gender.Male);
    //        ////b.SetBirthday(new DateTime(1985, 1, 1));
    //        ////b.TagForChildDirectedTreatment(false);
    //        ////b.AddExtra("color_bg", "9B30FF");
    //        //return b.Build();


    //        return new AdRequest.Builder()
    //            .AddTestDevice(AdRequest.TestDeviceSimulator)
    //            //.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
    //            //.AddKeyword("game")
    //            //.SetGender(Gender.Male)
    //            //.SetBirthday(new DateTime(1985, 1, 1))
    //            //.TagForChildDirectedTreatment(false)
    //            //.AddExtra("color_bg", "9B30FF")
    //            .Build();
    //    }


    //    /// <summary>
    //    /// 안드로이드 배너광고 아이디
    //    /// </summary>
    //    public string androidBannerid;
    //    /// <summary>
    //    /// 아이폰 배너 광고 아이디
    //    /// </summary>
    //    public string iphoneBannerid;
    //    /// <summary>
    //    /// 테스트 배너 광고 아이디
    //    /// </summary>
    //    public string testBannerid = "ca-app-pub-3940256099942544/6300978111";

    //    /// <summary>
    //    /// 배너 광고 불러오기
    //    /// </summary>
    //    private void RequestBanner()
    //    {
    //        string adUnitId = "";
    //        //#if UNITY_EDITOR
    //        //        adUnitId = "unused";
    //        //#else

    //        //        if (Debug.isDebugBuild)
    //        //            adUnitId = testBannerid;
    //        //        else
    //        //        {
    //        //#if UNITY_ANDROID
    //        //            adUnitId = androidBannerid;
    //        //#elif UNITY_IPHONE
    //        //            adUnitId = iphoneBannerid;
    //        //#else
    //        //            adUnitId = "unexpected_platform";
    //        //#endif
    //        //        }
    //        //#endif
    //        //테스트 아이디일때
    //        if (testId)
    //        {
    //            adUnitId = testBannerid;
    //        }

    //        else
    //        {
    //            adUnitId = androidBannerid;
    //        }
    //        // Clean up banner ad before creating a new one.
    //        if (this.bannerView != null)
    //        {
    //            this.bannerView.Destroy();
    //        }

    //        // Create a 320x50 banner at the top of the screen.
    //        this.bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);

    //        // Register for ad events.
    //        this.bannerView.OnAdLoaded += this.HandleAdLoaded;
    //        this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
    //        this.bannerView.OnAdOpening += this.HandleAdOpened;
    //        this.bannerView.OnAdClosed += this.HandleAdClosed;
    //        this.bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;

    //        // Load a banner ad.
    //        this.bannerView.LoadAd(this.CreateAdRequest());
    //    }

    //    /// <summary>
    //    /// 안드로이드 전면 광고 아이디
    //    /// </summary>
    //    public string androidInterstitialid;
    //    /// <summary>
    //    /// 아이폰 전면 광고 아이디
    //    /// </summary>
    //    public string iphoneInterstitialid;
    //    /// <summary>
    //    /// 테스트 전면 광고 아이디
    //    /// </summary>
    //    public string testInterstitialid = "ca-app-pub-3940256099942544/1033173712";

    //    public void RequestInterstitial()
    //    {

    //        string adUnitId = "";
    //        //#if UNITY_EDITOR
    //        //        adUnitId = "unused";
    //        //        txt.text = androidInterstitialid;
    //        //#else

    //        //        if (Debug.isDebugBuild)
    //        //            adUnitId = testInterstitialid;
    //        //        else
    //        //        {
    //        //            // These ad units are configured to always serve test ads.
    //        //#if UNITY_ANDROID
    //        //            adUnitId = androidInterstitialid;
    //        //        //테스트
    //        //         txt.text = androidInterstitialid;
    //        //#elif UNITY_IPHONE
    //        //            adUnitId = iphoneInterstitialid;
    //        //#else
    //        //            adUnitId = "unexpected_platform";
    //        //#endif
    //        //        }
    //        //#endif
    //        if (testId)
    //        {
    //            adUnitId = testInterstitialid;
    //        }
    //        else
    //        {
    //            adUnitId = androidInterstitialid;
    //        }

    //        // Create an interstitial.
    //        this.interstitial = new InterstitialAd(testInterstitialid);

    //        // Register for ad events.
    //        this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
    //        this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
    //        this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
    //        this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
    //        this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;

    //        // Load an interstitial ad.
    //        this.interstitial.LoadAd(this.CreateAdRequest());
    //    }

    //    /// <summary>
    //    /// 안드로이드 보상 광고 아이디
    //    /// </summary>
    //    public string androidRewardedid;
    //    /// <summary>
    //    /// 아이폰 보상 광고 아이디
    //    /// </summary>
    //    public string iphoneRewardedid;
    //    /// <summary>
    //    /// 테스트 보상 광고 아이디
    //    /// </summary>
    //    public string testRewardedid;// = "ca-app-pub-3940256099942544/5224354917";

    //    public void CreateAndLoadRewardedAd()
    //    {

    //        string adUnitId = "";
    //        //#if UNITY_EDITOR
    //        //        adUnitId = "unused";
    //        //#else

    //        //        if (Debug.isDebugBuild)
    //        //            adUnitId = testRewardedid;
    //        //        else
    //        //        {
    //        //            // These ad units are configured to always serve test ads.
    //        //#if UNITY_ANDROID
    //        //            adUnitId = androidRewardedid;
    //        //#elif UNITY_IPHONE
    //        //            adUnitId = iphoneRewardedid;
    //        //#else
    //        //            adUnitId = "unexpected_platform";
    //        //#endif
    //        //        }
    //        //#endif
    //        if (testId)
    //        {
    //            adUnitId = testRewardedid;
    //        }
    //        else
    //        {
    //            adUnitId = androidRewardedid;
    //        }

    //        // Create new rewarded ad instance.
    //        this.rewardedAd = new RewardedAd(adUnitId);

    //        // Called when an ad request has successfully loaded.
    //        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
    //        // Called when an ad request failed to load.
    //        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
    //        // Called when an ad is shown.
    //        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
    //        // Called when an ad request failed to show.
    //        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
    //        // Called when the user should be rewarded for interacting with the ad.
    //        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
    //        // Called when the ad is closed.
    //        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

    //        // Create an empty ad request.
    //        AdRequest request = this.CreateAdRequest();
    //        // Load the rewarded ad with the request.
    //        this.rewardedAd.LoadAd(request);
    //    }

    //    //public void ShowBanner(bool b)
    //    //{

    //    //}

    //    /// <summary>
    //    /// 전면 광고 출력
    //    /// </summary>
    //    public void ShowInterstitial()
    //    {

    //        if (this.interstitial.IsLoaded())
    //        {
    //            this.interstitial.Show();
    //        }
    //        else
    //        {
    //            MonoBehaviour.print("Interstitial is not ready yet");
    //        }
    //    }

    //    /// <summary>
    //    /// 보상 광고 출력
    //    /// </summary>
    //    public void ShowRewardedAd()
    //    {

    //        if (this.rewardedAd.IsLoaded())
    //        {
    //            this.rewardedAd.Show();
    //        }
    //        else
    //        {
    //            MonoBehaviour.print("Rewarded ad is not ready yet");
    //        }
    //    }

    //    #region Banner callback handlers

    //    public void HandleAdLoaded(object sender, EventArgs args)
    //    {
    //        MonoBehaviour.print("HandleAdLoaded event received");
    //    }

    //    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    //    {
    //        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
    //    }

    //    public void HandleAdOpened(object sender, EventArgs args)
    //    {
    //        MonoBehaviour.print("HandleAdOpened event received");
    //    }

    //    public void HandleAdClosed(object sender, EventArgs args)
    //    {
    //        MonoBehaviour.print("HandleAdClosed event received");
    //    }

    //    public void HandleAdLeftApplication(object sender, EventArgs args)
    //    {
    //        MonoBehaviour.print("HandleAdLeftApplication event received");
    //    }

    //    #endregion

    //    #region Interstitial callback handlers

    //    public void HandleInterstitialLoaded(object sender, EventArgs args)
    //    {
    //        MonoBehaviour.print("HandleInterstitialLoaded event received");
    //    }

    //    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    //    {
    //        MonoBehaviour.print(
    //            "HandleInterstitialFailedToLoad event received with message: " + args.Message);
    //    }

    //    public void HandleInterstitialOpened(object sender, EventArgs args)
    //    {
    //        MonoBehaviour.print("HandleInterstitialOpened event received");
    //    }

    //    public void HandleInterstitialClosed(object sender, EventArgs args)
    //    {
    //        testText.text = "들어옴";
    //        DebugX.Log("들옴");
    //        // Clean up interstitial ad before creating a new one.
    //        if (this.interstitial != null)
    //        {
    //            this.interstitial.Destroy();
    //        }

    //        this.RequestInterstitial();

    //        MonoBehaviour.print("HandleInterstitialClosed event received");
    //    }

    //    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    //    {
    //        MonoBehaviour.print("HandleInterstitialLeftApplication event received");
    //    }

    //    #endregion

    //    #region RewardedAd callback handlers

    //    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    //    {
    //        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    //    }

    //    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    //    {
    //        MonoBehaviour.print(
    //            "HandleRewardedAdFailedToLoad event received with message: " + args.Message);
    //    }

    //    public void HandleRewardedAdOpening(object sender, EventArgs args)
    //    {
    //        MonoBehaviour.print("HandleRewardedAdOpening event received");
    //    }

    //    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    //    {
    //        MonoBehaviour.print(
    //            "HandleRewardedAdFailedToShow event received with message: " + args.Message);
    //    }

    //    public void HandleRewardedAdClosed(object sender, EventArgs args)
    //    {
    //        MonoBehaviour.print("HandleRewardedAdClosed event received");
    //    }

    //    public void HandleUserEarnedReward(object sender, Reward args)
    //    {
    //        string type = args.Type;
    //        double amount = args.Amount;
    //        MonoBehaviour.print(
    //            "HandleRewardedAdRewarded event received for "
    //                        + amount.ToString() + " " + type);
    //    }

    //    #endregion
}