using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectWindow : MonoBehaviour
{
    [SerializeField] private SelectableItem[] _selectableItems;
    [SerializeField] private Button _acceptButton;
    [SerializeField] private Button _cancelButton;

    private AttackWindow _attackWindow;
    private IItemsPoolService _itemPool;
    private IStartBattleService _battleService;

    private void OnDestroy()
    {
        _acceptButton.onClick.RemoveListener(StartBattle);
        _cancelButton.onClick.RemoveListener(Hide);
        _battleService.ItemSelected -= EnableButton;
    }
            

    public void Construct(IItemsPoolService itemsPoolService, IStartBattleService battleService, AttackWindow attackWindow)
    {
        _attackWindow = attackWindow;
        _itemPool = itemsPoolService;
        _battleService = battleService;

        _battleService.ItemSelected += EnableButton;
        _cancelButton.onClick.AddListener(Hide);
        _acceptButton.onClick.AddListener(StartBattle);

        foreach (var selectableItem in _selectableItems)
            selectableItem.Construct(battleService);

        Hide();
    }

    public void Show()
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

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void StartBattle()
    {
        _battleService.SelectEnemyItems();
        _attackWindow.Show();
        Hide();
    }

    private void EnableButton() => 
        _acceptButton.interactable =
        _battleService.SelectedItems.Count > 0;
}
