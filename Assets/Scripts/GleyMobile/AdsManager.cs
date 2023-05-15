using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour
{
    public UnityEvent _alVerRewardedAd;
    [SerializeField]
    private int _clicksForAd;
    [SerializeField]
    private int _clicks;
    [SerializeField]
    private LayoutElement[] layoutElements;
    [SerializeField]
    private bool _debugMode;

    private void Awake() 
    {
        if (_debugMode) return;
        Advertisements.Instance.Initialize();
        Advertisements.Instance.debug = false;
    }

    private void Start() 
    {
        Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM, BannerType.Adaptive);
        SetBannerHeight();
    }

    private void SetBannerHeight()
    {
        foreach (var item in layoutElements)
        {
            item.minHeight = 131;
            item.preferredHeight = 131;
        }
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