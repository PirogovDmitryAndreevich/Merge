using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class AttackTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private float _duration = 60f;

    public event Action TimerFinished;

    private Coroutine _timerCoroutine;

    public void StartTimer()
    {
        if (_timerCoroutine != null)
            StopCoroutine(_timerCoroutine);

        gameObject.SetActive(true);
        _timerCoroutine = StartCoroutine(RunTimer());
    }

    public void StopTimer()
    {
        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;
        }

        gameObject.SetActive(false);
    }

    private IEnumerator RunTimer()
    {
        float remainingTime = _duration;
        int displayedSeconds = -1;

        while (remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;

            int seconds = Mathf.CeilToInt(Mathf.Clamp(remainingTime, 0f, _duration));

            if (seconds != displayedSeconds)
            {
                displayedSeconds = seconds;
                UpdateDisplay(seconds);
            }

            yield return null;
        }

        UpdateDisplay(0);

        TimerFinished?.Invoke();
        StopTimer();
    }

    private void UpdateDisplay(int seconds)
    {
        _timerText.text = seconds.ToString();
    }
}