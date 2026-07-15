using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image _advIcon;
    [SerializeField] private Image _alert;
    [SerializeField] private TMP_Text _alertCounter;

    private IBonusHandler _bonusHandle;
    private bool _isActive;

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClicked);

        _bonusHandle.BonusCounterChanger -= UpdateView;
    }

    public void Construct(IBonusHandler bonusHandler)
    {
        _bonusHandle = bonusHandler;
        _button.onClick.AddListener(OnButtonClicked);

        _bonusHandle.BonusCounterChanger += UpdateView;

        _isActive = !_bonusHandle.IsBonusAvailable;
        UpdateView();
    }

    private void OnButtonClicked() => 
        _bonusHandle.OnClickedBonus();

    private void UpdateView()
    {
        if (_isActive != _bonusHandle.IsBonusAvailable)
        {
            _isActive = _bonusHandle.IsBonusAvailable;

            _alert.gameObject.SetActive(_isActive);

            _advIcon.gameObject.SetActive(!_isActive);
            _canvasGroup.alpha = _isActive ? 1f : 0.5f;
        }

        if(_isActive)
            UpdateCounter(_bonusHandle.MultiplierCount);
    }

    private void UpdateCounter(int count) =>
        _alertCounter.text = count.ToString();
}
