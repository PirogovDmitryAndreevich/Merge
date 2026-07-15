using System;
using UnityEngine;

public interface ICoinsCounterService : IService
{
    event Action MultiplierStarted;
    event Action MultiplierStopped;
    event Action<float> MultiplierTimeChanged;

    void AddCoins(int amount, Vector2 position, bool withMultiplier = false);
    void AddScore(int score, Vector2 position);
    void AddMultiplier();
}