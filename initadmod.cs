using UnityEngine;
using System.Collections;
using admob;
public class initadmod : MonoBehaviour {
    public static initadmod instance;
    // Use this for initialization
    void Start () {
        instance = this;
        Debug.Log("start unity demo-------------");
         initAdmob();
	}
    public string baner_Adr;
    public string fullbaner_Adr;
   
    public string baner_IOS ;
    public string fullbaner_IOS  ;
  
    Admob ad;
    //bool isAdmobInited = false;
    void initAdmob()
    {
        string adUnitIdbaner  ;
        string adUnitIdfull ;
        //  isAdmobInited = true;
#if UNITY_EDITOR
          adUnitIdbaner = "baner_Adr";
             adUnitIdfull = "fullbaner_Adr";
#elif UNITY_ANDROID
              adUnitIdbaner = baner_Adr;
              adUnitIdfull = fullbaner_Adr;
#elif UNITY_5 || UNITY_IOS || UNITY_IPHONE
               adUnitIdbaner = baner_IOS;
               adUnitIdfull = fullbaner_IOS;
#else
                adUnitIdbaner = baner_Adr;
               adUnitIdfull = fullbaner_Adr;
#endif

        // Create a 320x50 banner at the top of the screen.
        // bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);
        // bannerView.LoadAd(createAdRequest());
        ad = Admob.Instance();
            ad.bannerEventHandler += onBannerEvent;
            ad.interstitialEventHandler += onInterstitialEvent;
            ad.rewardedVideoEventHandler += onRewardedVideoEvent;
            ad.nativeBannerEventHandler += onNativeBannerEvent;
            ad.initAdmob(adUnitIdbaner, adUnitIdfull);
            //   ad.setTesting(true);
            Debug.Log("admob inited -------------");
        
    }
    public bool ShowBanerOnPlay;
    public bool _ShowFullOnBackToMainMenu;
    public bool _ShowFullNowOnOpenGame;
    public int ShowFullOndie;

    /// <summary>
    ///  hiện baner nhỏ góc trên màn hình lúc chạy
    /// </summary>
    public void ShowBanerOnPlayGame()
    {
        if (ShowBanerOnPlay)
        {
            ShowBaner();
        }
    }
    /// <summary>
    /// hiện baner lúc chết
    /// sau Showfullondie số lần chết mới cho hiện
    /// </summary>
    public void ShowFullOnDie()
    {
        if (UImanager.uimanager.showbane >= ShowFullOndie-1)
        {
            showInterstitial();
            UImanager.uimanager.showbane = 0;
        }
    }
    /// <summary>
    /// hiện qc khi đến menu chính
    /// </summary>
    public void ShowFullOnBackToMainMenu()
    {
        if (_ShowFullOnBackToMainMenu)
        {
            showInterstitial();
        }
    }
    /// <summary>
    /// hiện quảng cáo khi mới mở game ra khi hết mục dowload
    /// </summary>
    public void ShowFullNowOnOpenGame()
    {
        if (_ShowFullNowOnOpenGame)
        {
            showInterstitial();
        }
    }
    void showInterstitial()
    {
        if (ad.isInterstitialReady())
        {
            ad.showInterstitial();
        }
        else
        {
            ad.loadInterstitial();
        }
    }
     void ShowBaner()
    {
        Admob.Instance().showBannerRelative(AdSize.SmartBanner, AdPosition.TOP_CENTER, 0);
    }

  public    void HideBaner()
    {
        Admob.Instance().removeBanner();
    }

 //   void OnGUI(){
 //       if (GUI.Button(new Rect(120, 0, 100, 60), "showInterstitial"))
 //       {
          
 //           if (ad.isInterstitialReady())
 //           {
 //               ad.showInterstitial();
 //           }
 //           else
 //           {
 //               ad.loadInterstitial();
 //           }
 //       }
 //       if (GUI.Button(new Rect(240, 0, 100, 60), "showRewardVideo"))
 //       {
            
 //           if (ad.isRewardedVideoReady())
 //           {
 //               ad.showRewardedVideo();
 //           }
 //           else
 //           {
            	
 //           		//ad.loadRewardedVideo("");
 //               ad.loadRewardedVideo("");
 //           }
 //       }
 //       if (GUI.Button(new Rect(0, 100, 100, 60), "showbanner"))
 //       {
 //           Admob.Instance().showBannerRelative(AdSize.Banner, AdPosition.BOTTOM_CENTER, 0);
 //       }
 //       if (GUI.Button(new Rect(120, 100, 100, 60), "showbannerABS"))
 //       {
 //           Admob.Instance().showBannerAbsolute(AdSize.Banner, 0, 300);
 //       }
 //       if (GUI.Button(new Rect(240, 100, 100, 60), "removebanner"))
 //       {
 //           Admob.Instance().removeBanner();
 //       }
 //       string nativeBannerID = "";//
 //      // string nativeBannerID = "";//google
 //       if (GUI.Button(new Rect(0, 200, 100, 60), "showNative"))
 //       {
            
 //           Admob.Instance().showNativeBannerRelative(new AdSize(320,120), AdPosition.BOTTOM_CENTER, 0,nativeBannerID);
 //       }
 //       if (GUI.Button(new Rect(120, 200, 100, 60), "showNativeABS"))
 //       {
 //           Admob.Instance().showNativeBannerAbsolute(new AdSize(320,120), 0, 300, nativeBannerID);
 //       }
 //       if (GUI.Button(new Rect(240, 200, 100, 60), "removeNative"))
 //       {
 //           Admob.Instance().removeNativeBanner();
 //       }
	//}
    void onInterstitialEvent(string eventName, string msg)
    {
        Debug.Log("handler onAdmobEvent---" + eventName + "   " + msg);
        if (eventName == AdmobEvent.onAdLoaded)
        {
            Admob.Instance().showInterstitial();
        }
    }
    void onBannerEvent(string eventName, string msg)
    {
        Debug.Log("handler onAdmobBannerEvent---" + eventName + "   " + msg);
    }
    void onRewardedVideoEvent(string eventName, string msg)
    {
        Debug.Log("handler onRewardedVideoEvent---" + eventName + "   " + msg);
    }
    void onNativeBannerEvent(string eventName, string msg)
    {
        Debug.Log("handler onAdmobNativeBannerEvent---" + eventName + "   " + msg);
    }
}
