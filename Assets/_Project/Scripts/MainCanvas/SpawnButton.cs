using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _price;

    private IProgressService _progress;
    private ISpawnButtonService _spawnService;

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonPressed);
        _progress.Progress.Player.CoinsChanged -= OnCoinsChanged;
        _spawnService.ViewChanged -= ChangeView;
        _spawnService.ItemChanged -= Initialize;
    }

    public void Construct(IProgressService progressService, ISpawnButtonService spawnService)
    {
        _progress = progressService;
        _spawnService = spawnService;

        _button.onClick.AddListener(OnButtonPressed);
        _progress.Progress.Player.CoinsChanged += OnCoinsChanged;
        _spawnService.ViewChanged += ChangeView;
        _spawnService.ItemChanged += Initialize;
    }

    private void Initialize()
    {
        _icon.sprite = _spawnService.CurrentItem.Sprite;
        _price.text = _spawnService.CurrentItem.Price.ToString();
    }

    private void OnCoinsChanged() =>
        _spawnService.CheckView();

    private void ChangeView(bool isActive) => 
        _price.color = isActive ? Color.black : Color.red;

    private void OnButtonPressed()
    {
        if (_spawnService.HaveCoinsForBuyItem)
            _spawnService.Spawn();
        else
            _spawnService.CheckView();
    }
}
