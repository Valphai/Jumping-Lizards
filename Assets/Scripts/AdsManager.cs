using UnityEngine.Advertisements;
using UnityEngine;
using DataManagement;
using System.Collections;
using UI;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private int cashFromVideo;
    [SerializeField] private ScoreManager scoreManager;

#if UNITY_IOS
    string gameID = "4294664";
    string rewarded = "Rewarded_iOS";
    string banner = "Banner_iOS";
#else
    string gameID = "4294665";
    string rewarded = "Rewarded_Android";
    string banner = "Banner_Android";

#endif
    void Start()
    {
        Advertisement.Initialize(gameID);
        Advertisement.AddListener(this);
        StartCoroutine(RepeatShowBanner());
    }
    public void PlayRewardedAd()
    {
        if (Advertisement.IsReady(rewarded))
            Advertisement.Show(rewarded);
    }

    public void ShowBanner()
    {
        if (Advertisement.IsReady(banner))
        {
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Show(banner);
        }
        else
            StartCoroutine(RepeatShowBanner());
    }
    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }
    private IEnumerator RepeatShowBanner()
    {
        yield return new WaitForSeconds(1);
        ShowBanner();
    }
    void IUnityAdsListener.OnUnityAdsReady(string placementId)
    {
        // Debug.Log("x");
        //throw new System.NotImplementedException();
    }

    void IUnityAdsListener.OnUnityAdsDidError(string message)
    {
        // Debug.Log("y");
        //throw new System.NotImplementedException();
    }

    void IUnityAdsListener.OnUnityAdsDidStart(string placementId)
    {
        // Debug.Log("z");
        //throw new System.NotImplementedException();
    }

    void IUnityAdsListener.OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == rewarded && showResult == ShowResult.Finished)
        {
            SaveData.DataSave.Cash += cashFromVideo;
            scoreManager.UpdateText();
        }
    }
}
