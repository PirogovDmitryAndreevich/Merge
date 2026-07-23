using System;

public interface IInterstitialAdvService : IService
{
    event Action ShowInterstitial;

    void ShowAdv();
    void OnHideTimer();
}