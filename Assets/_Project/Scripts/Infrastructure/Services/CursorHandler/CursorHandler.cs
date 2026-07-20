using System;
using System.Linq;

public class CursorHandler : ICursorHandler
{
    private readonly IItemsPoolService _itemsPoolService;

    public event Action<Item> ShowingItemCursor;
    public event Action ShowingButtonCursor;
    public event Action HidingCursor;

    public bool IsButtonCursorEnable { get; private set; }
    public float NotActionTimer { get; set; }

    public CursorHandler(IItemsPoolService itemsPoolService) => 
        _itemsPoolService = itemsPoolService;

    public void ShowMaxItem()
    {
        Item item = _itemsPoolService.ItemsOnScene
                    .OrderByDescending(x => x.ID)
                    .FirstOrDefault();

        item.EnableCursor();
        ShowingItemCursor?.Invoke(item);
    }

    public void ShowButtonClick()
    {
        IsButtonCursorEnable = true;
        ShowingButtonCursor?.Invoke();
    }

    public void HideCursor()
    {
        IsButtonCursorEnable = false;
        HidingCursor?.Invoke();
    }
}