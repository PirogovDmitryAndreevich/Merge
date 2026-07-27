using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class RewardSpawnButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _advCounter;
    [SerializeField] private Slider _slider;

    private ISpawnButtonService _spawnService;
    private IProgressService _progressService;

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
        _spawnService.ItemChanged -= Initialize;
        _progressService.Progress.Player.ViewedAdvChanged -= UpdateCounter;
    }

    public void Construct(ISpawnButtonService spawnService, IProgressService progressService)
    {
        _spawnService = spawnService;
        _progressService = progressService;

        _button.onClick.AddListener(OnButtonClicked);
        _spawnService.ItemChanged += Initialize;
        _progressService.Progress.Player.ViewedAdvChanged += UpdateCounter;

        _slider.minValue = 0;
        _slider.maxValue = 1;

        UpdateCounter();
    }

    private void Initialize() => 
        _icon.sprite = _spawnService.BonusItem.Sprite;

    private void OnButtonClicked() => 
        YG2.RewardedAdvShow(RewardType.Item.ToString());

    private void UpdateCounter()
    {
        int current = _progressService.Progress.Player.ViewedAdv;
        int target = 10;

        _advCounter.text = $"{current} / {target}";
        _slider.value = (float)current / target;
    }
}