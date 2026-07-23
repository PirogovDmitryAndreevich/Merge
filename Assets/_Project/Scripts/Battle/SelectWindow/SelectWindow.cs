using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectWindow : MonoBehaviour
{
    [SerializeField] private SelectableItem[] _selectableItems;
    [SerializeField] private Button _acceptButton;
    [SerializeField] private Button _cancelButton;

    private IItemsPoolService _itemPool;
    private IBattleService _battleService;

    private void OnDestroy()
    {
        _acceptButton.onClick.RemoveListener(OnAccept);
        _cancelButton.onClick.RemoveListener(OnCancel);
        _battleService.ItemSelected -= EnableButton;
        _battleService.StartSelectingItems -= Show;
    }     

    public void Construct(IItemsPoolService itemsPoolService, IBattleService battleService)
    {
        _itemPool = itemsPoolService;
        _battleService = battleService;

        _battleService.StartSelectingItems += Show;
        _battleService.ItemSelected += EnableButton;
        _cancelButton.onClick.AddListener(OnCancel);
        _acceptButton.onClick.AddListener(OnAccept);

        foreach (var selectableItem in _selectableItems)
            selectableItem.Construct(battleService);

        Hide();
    }

    private void Show()
    {
        EnableButton();

        List<Item> sortedItems = new List<Item>();

        foreach (Item item in _itemPool.ItemsOnScene)
        {
            int index = 0;

            while (index < sortedItems.Count &&
                   sortedItems[index].ItemData.ID > item.ItemData.ID)
            {
                index++;
            }

            sortedItems.Insert(index, item);
        }

        for (int i = 0; i < _selectableItems.Length; i++)
        {
            if (i < sortedItems.Count)
            {
                _selectableItems[i].Show();
                _selectableItems[i].Initialize(sortedItems[i].ItemData);
            }
            else
            {
                _selectableItems[i].Hide();
            }
        }

        gameObject.SetActive(true);
    }

    private void OnCancel()
    {
        _battleService.OnCancel();
        Hide();
    }

    private void OnAccept()
    {
        _battleService.StartBattle();
        Hide();
    }

    private void Hide() =>
        gameObject.SetActive(false);

    private void EnableButton() => 
        _acceptButton.interactable =
        _battleService.SelectedItems.Count > 0;
}
