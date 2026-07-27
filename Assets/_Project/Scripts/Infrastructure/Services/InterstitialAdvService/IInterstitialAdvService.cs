using System;

public interface IInterstitialAdvService : IService
{
    public bool IsShowing { get; }

    event Action ShowInterstitial;

    void ShowAdv();
    void OnHideTimer();
}