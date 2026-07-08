using System;
using System.Collections;
using UnityEngine;

public class CoinsCounterService : ICoinsCounterService
{
    private readonly IProgressService _progress;
    private readonly IPoolService _pool;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly GameData _data;

    private bool IsMultiplier;
    float _timer;

    public event Action MultiplierStarted;
    public event Action MultiplierStopped;
    public event Action<float> MultiplierTimeChanged;

    public CoinsCounterService(IProgressService progress, IPoolService pool, ICoroutineRunner coroutineRunner, GameData data)
    {
        _progress = progress;
        _pool = pool;
        _coroutineRunner = coroutineRunner;
        _data = data;
    }

    public void AddCoins(int amount, Vector2 position)
    {
        if (IsMultiplier)
            amount *= _data.MultiplierValue;

        _progress.Progress.Player.AddCoins(amount);
        PlayValue(amount, position);
    }

    public void AddMultiplier(BonusItem item)
    {
        _pool.Return(item);

        if (IsMultiplier)
        {
            _timer = _data.MultiplierTimer;
            return;
        }

        IsMultiplier = true;
        MultiplierStarted?.Invoke();

        _coroutineRunner.StartCoroutine(MultiplierCoroutine());
    }

    private void StopMultiplier()
    {
        IsMultiplier = false;
        _timer = 0;
        MultiplierStopped?.Invoke();
    }

    private void PlayValue(int profit, Vector2 position)
    {
        ProfitEffect effect = _pool.Get<ProfitEffect>();
        effect.Play(profit, position);
    }

    private IEnumerator MultiplierCoroutine()
    {
        _timer = _data.MultiplierTimer;

        while (_timer > 0)
        {
            _timer -= Time.deltaTime;

            float progress = Mathf.Clamp01(_timer / _data.MultiplierTimer);
            MultiplierTimeChanged?.Invoke(progress);

            yield return null;
        }

        StopMultiplier();
    }
}