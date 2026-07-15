using System;

public interface IBonusHandler : IService
{
    bool IsBonusAvailable { get; }

    int MultiplierCount { get; }

    event Action BonusCounterChanger;

    void AddBonus();
    void OnClickedBonus();
}