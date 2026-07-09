using UnityEngine;
using UnityEngine.UI;

public class SelectableItem : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;
    [SerializeField] private Toggle _toggle;

    private IStartBattleService _battleService;
    private ItemData _itemData;
    private bool _isSelected;

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    public void Construct(IStartBattleService battleService)
    {
        _battleService = battleService;
        _button.onClick.AddListener(OnButtonClicked);

        Hide();
    }

    public void Initialize(ItemData itemData)
    {
        _itemData = itemData;
        _icon.sprite = _itemData.Sprite;

        OnClickSelect(false);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void OnButtonClicked()
    {
        _isSelected = !_isSelected;
        OnClickSelect(_isSelected);
    }

    private void OnClickSelect(bool isSelected)
    {
        _isSelected = isSelected;

        if (isSelected)
        {
            if (!_battleService.TrySelectItem(_itemData))
            {
                _isSelected = !isSelected;                
                return;
            }
        }
        else
        {
            _battleService.DeselectItem(_itemData);
        }

        SetToggle(_isSelected);
    }

    private void SetToggle(bool isSelected) => 
        _toggle.SetIsOnWithoutNotify(isSelected);
}
