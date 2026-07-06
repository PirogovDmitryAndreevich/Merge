using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardSlider : MonoBehaviour
{
    private const int ZoneCount = 4;
    private readonly int[] _rewardMultipliers = { 2, 3, 4, 5 };

    [SerializeField] private Slider _boostSlider;
    [SerializeField] private Button _rewardAdvButton;
    [SerializeField] private TMP_Text _rewardText;
    [SerializeField] private TMP_Text _multiplierText;
    [SerializeField] private float _sliderSpeed = 1.5f;

    private Coroutine _sliderCoroutine;
    private int _reward;

    public event Action RewardButtonClicked;

    private void Awake() => 
        _rewardAdvButton.onClick
        .AddListener(OnRewardAdvButtonClick);

    private void OnDestroy() => 
        _rewardAdvButton.onClick
        .RemoveListener(OnRewardAdvButtonClick);

    private void OnDisable()
    {
        StopSlider();
        _rewardAdvButton.onClick.RemoveListener(OnRewardAdvButtonClick);
    }

    public void StartSlider(int reward)
    {
        StopSlider();
        _reward = reward;
        _boostSlider.value = 0;

        _sliderCoroutine = StartCoroutine(SliderRoutine());
    }

    private void StopSlider()
    {
        if (_sliderCoroutine != null)
        {
            StopCoroutine(_sliderCoroutine);
            _sliderCoroutine = null;
        }
    }

    private IEnumerator SliderRoutine()
    {
        float value = 0f;
        float direction = 1f;

        while (true)
        {
            value += direction * _sliderSpeed * Time.deltaTime;

            if (value >= 1f)
            {
                value = 1f;
                direction = -1f;
            }
            else if (value <= 0f)
            {
                value = 0f;
                direction = 1f;
            }

            _boostSlider.value = value;

            int multiplier = GetCurrentMultiplier();

            _multiplierText.text = $"x{multiplier}";
            _rewardText.text = (_reward * multiplier).ToString();

            yield return null;
        }
    }

    private void OnRewardAdvButtonClick()
    {
        StopSlider();

        int multiplier = GetCurrentMultiplier();
        int finalReward = _reward * multiplier;

        Debug.Log($"Reward x{multiplier} = {finalReward}");

        RewardButtonClicked?.Invoke();
    }

    private int GetCurrentMultiplier()
    {
        // Получаем индекс цветной зоны (0..3)
        int zoneIndex = Mathf.FloorToInt(_boostSlider.value * ZoneCount);

        // На случай value == 1
        zoneIndex = Mathf.Clamp(zoneIndex, 0, ZoneCount - 1);

        return _rewardMultipliers[zoneIndex];
    }
}
