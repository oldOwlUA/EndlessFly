using System;
using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using SylokHiddenCat;

public class ManagerGoogle : MonoBehaviour
{
//Banner ca-app-pub-3940256099942544/6300978111
//Interstitial ca-app-pub-3940256099942544/1033173712
//Rewarded Video  ca-app-pub-3940256099942544/5224354917
//Native Advanced ca-app-pub-3940256099942544/2247696110


    public static ManagerGoogle Instance;

    [Header("Inter Delta Time")]
    public float InterTimeDeleayMin, InterTimeDeleayMax;
    [Header("Android")]
    public string BannerIdAndroid = "ca-app-pub-3940256099942544/6300978111";
    public string InterstitialIdAndroid = "ca-app-pub-3940256099942544/1033173712";
    public string RewardIdAndroid;
    [Header("IOS")]
    public string BannerIdIOS = "";
    public string InterstitialIdIOS = "";
    public string RewardIdIOS = "";
    [Header("BanerSettings")]
    public bool BanerUse = true;
    public bool UseReward = false;
    public AdPosition BanerPosition;
	public bool isSmartBanner;

    private string BanneradUnitId = "";
    private string InteradUnitId = "";
    private string RewardadUnitId = "";
   
    private int _isAdvertsDisabled;
    private bool rewardBasedEventHandlersSet = false;

    private float last_banner_time = 0;
    private float banner_time = 110;

   // public static System.Action RewardedVideoSuccessful; // вызывается при успешном просмотре видео рекламы - на этот Action надо регистрировать колбек из стора. 

    // состояние рекламы
    public bool IsAdvertsDisabled
    {
        get { return _isAdvertsDisabled == 1; }
        private set { _isAdvertsDisabled = value ? 1 : 0; }
    }

    // ADS Entity
    public RewardBasedVideoAd rewardBasedVideo;
    private BannerView bannerView;
    private InterstitialAd interstitial;

    // Создание Синглтона и првоерка на дубликаты
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
            Debug.LogWarning("You have duplicate Google Managers AD in your scene!");
        }
    }

    // Встарте Инициализация плагинов
    private void Start()
    {
        InitAdmob();                               // инициализация рекламы
        GPGSManager.Instance.Authenticate();       // инициализация Игровых Сервисов 
       // Purchaser.Instance.UPurchaiseInit(); 
    }

    // регистрация евентов банера
    private void AddEventBanner()
    {
        // Called when an ad request has successfully loaded.
        bannerView.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        bannerView.OnAdOpening += HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        bannerView.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;
       
    }

    // регистрация евентов полноэкранного банера
    private void AddEventInter()
    { 
        // Called when the user returned from the app after an ad click.
        interstitial.OnAdClosed += InterstitialOnOnAdClosed;
        // Called when an ad request failed to load.
        interstitial.OnAdFailedToLoad += InterstitialOnOnAdFailedToLoad;
        // Called when an ad is clicked.
        interstitial.OnAdLeavingApplication += InterstitialOnOnAdLeavingApplication;
        // Called when an ad request has successfully loaded.
        interstitial.OnAdLoaded += InterstitialOnOnAdLoaded;
        // Called when an ad request has successfully opened.
        interstitial.OnAdOpening += InterstitialOnOnAdOpening;
    }

    // регистрация евентов ревард видео
    private void AddRewarded()
    {
        // Reward based video instance is a singleton. Register handlers once to
        // avoid duplicate events.
        if (!rewardBasedEventHandlersSet)
        {
            // Ad event fired when the rewarded video ad
            // has been received.
            rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
            // has failed to load.
            rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
            // is opened.
            rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
            // has started playing.
            rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
            // has rewarded the user.
            rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
            // is closed.
            rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
            // is leaving the application.
            rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

            rewardBasedEventHandlersSet = true;
        }
    }


    // показать фулскрин банер
    public void ShowFullscreenBanner()
    {
        if (!IsAdvertsDisabled)
        {
            float time = Time.time;
            if (time - last_banner_time >= banner_time)
            {
                last_banner_time = time;
                //print("InterShow");
                banner_time = UnityEngine.Random.Range(InterTimeDeleayMin, InterTimeDeleayMax);
                if (interstitial.IsLoaded())
                {
                    interstitial.Show();
                    LoadInter(InteradUnitId);
                }
                else
                {
                    //TODO show home ads
                }
            }
        }
    }

    //показать видео
    public void ShowRewardedVedeo()
    {
        if (!UseReward) return;
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
        else {
            LoadReward(RewardadUnitId);
        }
    }


    // инициализация адс
    private void InitAdmob()
    {
        if (PlayerPrefs.HasKey("AdmobDisabled"))
        {
            _isAdvertsDisabled = PlayerPrefs.GetInt("AdmobDisabled");
        }
        else
        {
            _isAdvertsDisabled = 0;
            PlayerPrefs.SetInt("AdmobDisabled", _isAdvertsDisabled);
        }

        if (IsAdvertsDisabled)
        {
           return;
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            BanneradUnitId = BannerIdAndroid;
            InteradUnitId = InterstitialIdAndroid;
            RewardadUnitId = RewardIdAndroid;
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            BanneradUnitId = BannerIdIOS;
            InteradUnitId = InterstitialIdIOS;
            RewardadUnitId = RewardIdIOS;
        }

        if(BanerUse) LoadBanner(BanneradUnitId);
        LoadInter(InteradUnitId);
        if(UseReward) LoadReward(RewardadUnitId);
    }
    // загрузка интера
    private void LoadInter(string adUnitID)
    {
        interstitial = new InterstitialAd(adUnitID);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
        AddEventInter();
    }
    // загрузка бангера
    private void LoadBanner(string adUnitID)
    {
        // Create  banner .
		if (isSmartBanner) {
			bannerView = new BannerView (adUnitID, AdSize.SmartBanner, BanerPosition);
		} else {
			bannerView = new BannerView (adUnitID, AdSize.Banner, BanerPosition);
		}
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);
        AddEventBanner();
    }

    private void LoadReward(string adUnitID)
    {
        rewardBasedVideo = RewardBasedVideoAd.Instance;
        AdRequest request = new AdRequest.Builder().Build();
        rewardBasedVideo.LoadAd(request, adUnitID);
        AddRewarded();
    }


    // поменять положение банера
    public void RepositionBanner(AdPosition adPosition)
    {
        bannerView.Destroy();
        BanerPosition = adPosition;
        LoadBanner(BanneradUnitId);
    }
    // скрыть банер
    public void HideSmallBanner()
    {
        if(BanerUse)
            bannerView.Hide();
    }
    // показать скрытый банер
    public void ShowSmallBanner()
    {
        if (!IsAdvertsDisabled && BanerUse)
        {
            bannerView.Show();
        }
    }
    //дестроит все ад юниты
    public void DestroyAllAds()
    {
        if(BanerUse) bannerView.Destroy();
        if(interstitial !=null) interstitial.Destroy();

    }
    //отключение рекламы
    public void DisableAdmob()
    {
        IsAdvertsDisabled = true;
        PlayerPrefs.SetInt("AdmobDisabled", _isAdvertsDisabled);
        DestroyAllAds();
    }


    /////////////////////////////////////// BANNER EVENTS ////////////////////////////////////////////// 
    private void HandleOnAdLeavingApplication(object sender, EventArgs e)
    {
        print("OnAdLeavingApplication event received.");
        // Handle the ad loaded event.
    }
    private void HandleOnAdClosed(object sender, EventArgs e)
    {
        print("OnAdClosed event received.");
        // Handle the ad loaded event.
    }
    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        print("OnAdOpened event received.");
        // Handle the ad loaded event.
    }
    private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        print("OnAdFailedToLoad event received.");
        // Handle the ad loaded event.
    }

    private void HandleOnAdLoaded(object sender, EventArgs e)
    {
        print("OnAdLoaded event received.");
        // Handle the ad loaded event.
    }


    /////////////// INTER  ////////////////////////////////////////
    // When interstitial window opens
    private void InterstitialOnOnAdOpening(object sender, EventArgs eventArgs)
    {
        Time.timeScale = 0;
        AudioListener.volume = 0;
    }
    // Inter is Loaded
    private void InterstitialOnOnAdLoaded(object sender, EventArgs eventArgs)
    {

    }
    // The player clicked an interstitial advert and the app has minimized
    private void InterstitialOnOnAdLeavingApplication(object sender, EventArgs eventArgs)
    {

       
    }
    // Failed to receive interstitial advert (e.g no internet connection)
    private void InterstitialOnOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs adFailedToLoadEventArgs)
    {

        
    }
    // When interstitial window is closed (Via hardware back button or clicking the X)
    private void InterstitialOnOnAdClosed(object sender, EventArgs eventArgs)
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
        LoadInter(InteradUnitId);
    }

    //////////////////////////////////////REWARDED CALLBACK //////////////////////////////////////////////////////////

    private void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        Debug.Log("Add revard");
        UIController.Instance.RevardedAds();
        //string type = args.Type;
        //double amount = args.Amount;
        //print("User rewarded with: " + amount.ToString() + " " + type);
        //LoadReward(RewardadUnitId);
       // RewardedVideoSuccessful();
    }

    private void HandleRewardBasedVideoLeftApplication(object sender, EventArgs e)
    {
      
    }

    private void HandleRewardBasedVideoClosed(object sender, EventArgs e)
    {

        LoadReward(RewardadUnitId);
    }

    private void HandleRewardBasedVideoStarted(object sender, EventArgs e)
    {
        
    }

    private void HandleRewardBasedVideoOpened(object sender, EventArgs e)
    {
       
    }

    private void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        UIController.Instance.revardOk = false;
    }

    private void HandleRewardBasedVideoLoaded(object sender, EventArgs e)
    {
        UIController.Instance.revardOk = true;
    }

}
