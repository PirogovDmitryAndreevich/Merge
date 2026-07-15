using UnityEngine;
using UnityEngine.UI;

public class BonusView : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private ICoinsCounterService _coinsService;

    private void OnDestroy()
    {
        _coinsService.MultiplierStarted -= StartBonus;
        _coinsService.MultiplierStopped -= StopBonus;
        _coinsService.MultiplierTimeChanged -= UpdateBonusTimer;
    }

    public void Construct(ICoinsCounterService coinsCounterService)
    {
        _coinsService = coinsCounterService;

        _slider.minValue = 0f;
        _slider.maxValue = 1f;

        _coinsService.MultiplierStarted += StartBonus;
        _coinsService.MultiplierStopped += StopBonus;
        _coinsService.MultiplierTimeChanged += UpdateBonusTimer;

        StopBonus();
    }

    private void StartBonus()
    {
        _slider.gameObject.SetActive(true);
        gameObject.SetActive(true);
        _slider.value = 1f;
    }

    private void UpdateBonusTimer(float progress)
    {
        _slider.value = progress;
    }

    private void StopBonus()
    {
        gameObject.SetActive(false);
        _slider.gameObject.SetActive(false);
    }
}
