using UnityEngine;
using UnityEngine.UI;

public class SelectWindow : MonoBehaviour
{
    [SerializeField] private SelectableItem[] _selectableItems;
    [SerializeField] private Button _acceptButton;
    [SerializeField] private Button _cancelButton;

    private AttackWindow _attackWindow;
    private IItemsPoolService _itemPool;

    private void OnDestroy()
    {
        _acceptButton.onClick.RemoveListener(StartBattle);
        _cancelButton.onClick.RemoveListener(Hide);
    }

    private void Awake()
    {
        _cancelButton.onClick.AddListener(Hide);
        _acceptButton.onClick.AddListener(StartBattle);
    }

    public void Construct(IItemsPoolService itemsPoolService, IBattleService battleService, AttackWindow attackWindow)
    {
        _attackWindow = attackWindow;
        _itemPool = itemsPoolService;

        foreach (var selectableItem in _selectableItems)
            selectableItem.Construct(battleService);
    }

    public void Show()
    {
        for (int i = 0; i < _selectableItems.Length; i++)
        {
            if (i < _itemPool.ItemsOnScene.Count)
            {
                Item item = _itemPool.ItemsOnScene[i];

                _selectableItems[i].Show();
                _selectableItems[i].Initialize(item.ItemData);
            }
            else
            {
                _selectableItems[i].Hide();
            }
            
        }

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void StartBattle()
    {
        _attackWindow.Show();
        Hide();
    }
}
