using System;
using System.Linq;

public class CursorHandler : ICursorHandler
{
    private readonly IItemsPoolService _itemsPoolService;

    public event Action<Item> ShowingItemCursor;
    public event Action ShowingButtonCursor;
    public event Action<Item, Item> ShowingMergeItemsCursor;
    public event Action HidingCursor;

    public bool IsButtonCursorEnable { get; private set; }
    public bool IsEnableCursor { get; private set; }

    public CursorHandler(IItemsPoolService itemsPoolService) =>
        _itemsPoolService = itemsPoolService;

    public void ShowMaxItem()
    {
        Item item = _itemsPoolService.ItemsOnScene
                    .OrderByDescending(x => x.ID)
                    .FirstOrDefault();

        IsEnableCursor = true;
        ShowingItemCursor?.Invoke(item);
    }

    public void ShowButtonClick()
    {
        IsButtonCursorEnable = true;
        ShowingButtonCursor?.Invoke();
    }

    public void ShowMergeItems(Item first, Item second)
    {
        IsEnableCursor = true; 
        ShowingMergeItemsCursor?.Invoke(first, second);
    }

    public void HideCursor()
    {
        IsButtonCursorEnable = false;
        IsEnableCursor = false;
        HidingCursor?.Invoke();
    }
}