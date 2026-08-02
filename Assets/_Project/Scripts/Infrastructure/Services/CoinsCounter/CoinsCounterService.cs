using System;
using System.Collections;
using UnityEngine;

public class CoinsCounterService : ICoinsCounterService
{
    private readonly IProgressService _progress;
    private readonly IPoolService _pool;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly GameData _data;
    private readonly IHintService _hintService;
    private bool IsMultiplier;
    float _timer;

    public event Action MultiplierStarted;
    public event Action MultiplierStopped;
    public event Action<float> MultiplierTimeChanged;

    public CoinsCounterService(IProgressService progress, IPoolService pool,
        ICoroutineRunner coroutineRunner, GameData data, IHintService hintService)
    {
        _progress = progress;
        _pool = pool;
        _coroutineRunner = coroutineRunner;
        _data = data;
        _hintService = hintService;
    }

    public void AddCoins(int amount, Vector2 position, bool withMultiplier = false)
    {
        if (IsMultiplier && withMultiplier)
            amount *= _data.MultiplierValue;

        _progress.Progress.Player.AddCoins(amount);
        PlayCoins(amount, position);
        _hintService.ResetTimer();
    }

    public void AddScore(int score, Vector2 position)
    {
        ValueEffect effect = _pool.Get<ValueEffect>();

        _progress.Progress.Player.AddScore(score);
        effect.Play(score, position, ValueType.Score);
    }

    public void AddMultiplier()
    {
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

    private void PlayCoins(int profit, Vector2 position)
    {
        ValueEffect effect = _pool.Get<ValueEffect>();
        effect.Play(profit, position, ValueType.Coins);
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