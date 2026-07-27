using System;
using System.Diagnostics;

public class InterstitialAdvService : IInterstitialAdvService
{
    public InterstitialAdvService()
    {
        IsShowing = false;
    }

    public bool IsShowing { get; private set; }

    public event Action ShowInterstitial;

    public void ShowAdv()
    {
        IsShowing = true;
        ShowInterstitial?.Invoke();
    }

    public void OnHideTimer()
    {
        IsShowing = false;
    }
}