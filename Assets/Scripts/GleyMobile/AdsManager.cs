using UnityEngine;
using UnityEngine.Events;

public class AdsManager : MonoBehaviour
{
    public UnityEvent _alVerRewardedAd;
    [SerializeField]
    private int _clicksForAd;
    [SerializeField]
    private int _clicks;

    private void Awake() 
    {
        Advertisements.Instance.Initialize();
        Advertisements.Instance.debug = false;
    }

    private void Start() 
    {
        Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM, BannerType.Adaptive);
    }

    public void AnuncioPorTiempo()
    {
        Advertisements.Instance.ShowRewardedVideo((bool x) => { if (x) _alVerRewardedAd?.Invoke(); });
    }

    public void AumentarContadorClicks()
    {
        if (++_clicks < _clicksForAd) 
        {
            RatePopUp.instance.ShowPopUp();
            return;
        }
        _clicks = 0;
        Advertisements.Instance.ShowInterstitial();
    }
}