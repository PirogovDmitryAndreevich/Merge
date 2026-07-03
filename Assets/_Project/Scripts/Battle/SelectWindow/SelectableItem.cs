using UnityEngine;
using UnityEngine.UI;

public class SelectableItem : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Toggle _toggle;

    private IBattleService _battleService;
    private ItemData _itemData;

    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(OnToggleSelect);
    }

    public void Construct(IBattleService battleService)
    {
        _battleService = battleService;
        _toggle.onValueChanged.AddListener(OnToggleSelect);

        Hide();
    }

    public void Initialize(ItemData itemData)
    {
        _itemData = itemData;
        _icon.sprite = _itemData.Sprite;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void OnToggleSelect(bool isOn)
    {
        if (isOn)        
            _battleService.SelectItem(_itemData);        
        else        
            _battleService.DeselectItem(_itemData);        
    }
}
