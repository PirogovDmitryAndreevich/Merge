using System;

public interface IInterstitialAdvService : IService
{
    public bool IsShowing { get; }
    float Timer { get; set; }

    event Action ShowInterstitial;

    void ShowAdv();
    void OnHideTimer();
}