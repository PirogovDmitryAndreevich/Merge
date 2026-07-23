using System;

public class InterstitialAdvService : IInterstitialAdvService
{
    public event Action ShowInterstitial;

    public void ShowAdv()
    {
        ShowInterstitial?.Invoke();
    }

    public void OnHideTimer()
    {

    }
}