using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

using GooglePlayGames;
using GooglePlayGames.BasicApi;

using UnityEngine.UI;

public class RunningManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
{
    #region ADD Area
    private string _androidId= "123134";
    private string _iOsId= "123134";
    private string _actualId;
    private bool testMode = false;


    //ADDS IDS
    private string interstitialIdAndroid1 = "Interstitial_Android";
    private string interstitialIdIOS1 = "Interstitial_iOS";
    private string interstitialID1;


    //Banner
    private string bannerIdAndroid1 = "Banner_Android";
    private string bannerIdIOS1 = "Banner_iOS";
    private string bannerID1;

    //REWARD
    private string rewardIdAndroid1 = "Rewarded_Android";
    private string rewardIdIOS1 = "Rewarded_iOS";
    private string rewardID1;


    #endregion
    // Start is called before the first frame update

    //!!!!!!!!!!!!!!!!!TO DO HIDE IN INSPECTOR OTHER VALUES IF NOT TICKED!!!!!!!!!!!!!!!!!!!
    public bool sceneAdds=false;

    //public bool timedAdd = false;
    //public bool scoreAdd = false;

    public int scoreToShowAdd = 12;
    public int secondsToWaitUntilAdd=150;

    public static float timeFromTheStart =0;
    public static bool freeze = false;

    public static bool notified = false;

    public static bool isGooglePlayConected = false;

    public static bool roundWatchedAdds = false;
    private bool showBanner = false;

    public GameObject ADNotLoadedMessage;
    bool ADNotLoadedMessageIsLoaded = false;
    void Start()
    {
        //ADDS
        if (sceneAdds == true)
        {
            _actualId = (Application.platform == RuntimePlatform.Android) ? _androidId : _iOsId;
            Advertisement.Initialize(_actualId, testMode);

            interstitialID1 = (Application.platform == RuntimePlatform.Android) ? interstitialIdAndroid1 : interstitialIdIOS1;
            bannerID1 = (Application.platform == RuntimePlatform.Android) ? bannerIdAndroid1 : bannerIdIOS1;
            rewardID1 = (Application.platform == RuntimePlatform.Android) ? rewardIdAndroid1 : rewardIdIOS1;


            Advertisement.Load(interstitialID1, this);
            Advertisement.Load(rewardID1, this);

            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);

            Advertisement.Banner.Load();
        }

        //Connect to google Play
        if (isGooglePlayConected==false && Application.platform == RuntimePlatform.Android)
        {
            PlayGamesPlatform.Activate();
            PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) =>
             {
                 switch (result)
                 {
                     case SignInStatus.Success:
                         //Debug.Log("Conected");
                         isGooglePlayConected = true;
                         break;
                     default:
                         //Debug.Log("NotConected");
                         isGooglePlayConected = false;
                         break;
                 }
             });
        }


        //QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
        /*var resolution = Screen.currentResolution;
        var height = (int)(resolution.height * 0.65f);
        var width = (int)(resolution.width * 0.65f);
        Screen.SetResolution(height, width, true);*/
    }


    private void Update()
    {
        if (timeFromTheStart<secondsToWaitUntilAdd)
        { 
            timeFromTheStart += Time.deltaTime;
            //Debug.Log(timeFromTheStart);
        }

    }

    public void ShowBanner()
    {
        showBanner = true;
        StartCoroutine(ShowBannerWhenReady());
        //Advertisement.Banner.Show(bannerID1);
    }

    public void HideBanner()
    {
        showBanner = false;
        Advertisement.Banner.Hide();
        //Advertisement.Banner.
        Advertisement.Banner.Load();
    }
    IEnumerator ShowBannerWhenReady()
    {
        if (showBanner==true)
        {
            while (!Advertisement.IsReady(bannerID1))  //????????????? Check again later if problems
            {
                if (!Advertisement.Banner.isLoaded)
                {
                    Advertisement.Banner.Load(bannerID1);
                    yield return new WaitForSeconds(0.5f);
                }
                /*
                 * old code
                 if (!Advertisement.Banner.isLoaded)
                    Advertisement.Banner.Load();
                if (showBanner == true)
                    yield return new WaitForSeconds(0.5f);
                break;

                */
            }
        }
        if (showBanner==true)
            Advertisement.Banner.Show(bannerID1);
        yield return null;
    }
    public void PlayTimedAdd()
    {
        if (sceneAdds==true)
        {
            //Debug.Log("TIMED "+ timeFromTheStart);
            if ((int)timeFromTheStart == secondsToWaitUntilAdd)
            {
                //Debug.Log("HELLO!");
                if (Advertisement.IsReady(interstitialID1))
                    Advertisement.Show(interstitialID1, this);
                Advertisement.Load(interstitialID1, this);
                timeFromTheStart = 0;
                //Debug.Log("TIMED ADD!");
            }
        }

    }

    public void PlayScoreAdd(int _score)
    {
        if (sceneAdds == true)
        {
            if (_score >= scoreToShowAdd && roundWatchedAdds==false)
            {
                if (Advertisement.IsReady(interstitialID1))
                    Advertisement.Show(interstitialID1, this);
                Advertisement.Load(interstitialID1, this);
                //Debug.Log("SCORED ADD!");
                timeFromTheStart = 0;
            }
        }

    }

    IEnumerator ShowADNotLoadedMessageForTime()
    {
        ADNotLoadedMessage.SetActive(true);
        yield return new WaitForSeconds(2f);
        ADNotLoadedMessage.SetActive(false);
        ADNotLoadedMessageIsLoaded = false;
    }

    public bool IsRewardAddReady()
    {
            bool isReady = Advertisement.IsReady(rewardID1);
            if (isReady == false)
            {
                Advertisement.Load(rewardID1, this);
                //Show Message for a few seconds
                if (ADNotLoadedMessageIsLoaded==false)
                {
                    ADNotLoadedMessageIsLoaded = true;
                    StartCoroutine(ShowADNotLoadedMessageForTime());
                }
                //if (no)
            }
            return isReady;
    }
    public void PlayFullVideoAdd()
    {
        if (sceneAdds == true)
        {
            //timeFromTheStart = 0;
            if (Advertisement.IsReady(rewardID1))
            {
                Advertisement.Show(rewardID1, this);
                roundWatchedAdds = true;
            }

            Advertisement.Load(rewardID1, this);
            //Debug.Log("FULL ADD!");
        }

    }

    //ADDS AREA EXCEPTION AREA
    void IUnityAdsLoadListener.OnUnityAdsAdLoaded(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    void IUnityAdsLoadListener.OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        //throw new System.NotImplementedException();
    }

    void IUnityAdsShowListener.OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        //throw new System.NotImplementedException();
    }

    void IUnityAdsShowListener.OnUnityAdsShowStart(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    void IUnityAdsShowListener.OnUnityAdsShowClick(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    void IUnityAdsShowListener.OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        //throw new System.NotImplementedException();
        if (placementId== rewardID1)
        {
            ADNotLoadedMessage.SetActive(false);
            ADNotLoadedMessageIsLoaded = false;
            Advertisement.Load(rewardID1, this);
            //Maybe better check rewarded video? Idk
        }


        if (placementId== interstitialID1)
            Advertisement.Load(interstitialID1, this);

    }

    public void OnInitializationComplete()
    {
        //throw new System.NotImplementedException();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        //throw new System.NotImplementedException();
    }
    // Update is called once per frame

}
