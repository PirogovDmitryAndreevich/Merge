using System;

public class InterstitialAdvService : IInterstitialAdvService
{
    private const float TimerForInterstitial = 60f;

    public InterstitialAdvService()
    {
        IsShowing = false;
        Timer = TimerForInterstitial;
    }

    public bool IsShowing { get; private set; }
    public float Timer { get; set; }

    public event Action ShowInterstitial;

    public void ShowAdv()
    {
        IsShowing = true;
        ShowInterstitial?.Invoke();
    }

    public void OnHideTimer()
    {
        IsShowing = false;
        Timer = TimerForInterstitial;
    }
}