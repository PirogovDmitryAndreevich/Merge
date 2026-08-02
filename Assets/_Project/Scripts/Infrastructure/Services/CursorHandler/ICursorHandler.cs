using System;

public interface ICursorHandler : IService
{
    event Action<Item> ShowingItemCursor;
    event Action<Item, Item> ShowingMergeItemsCursor;
    event Action ShowingButtonCursor;
    event Action HidingCursor;

    bool IsButtonCursorEnable { get; }
    bool IsEnableCursor { get; }

    void HideCursor();
    void ShowButtonClick();
    void ShowMaxItem();
    void ShowMergeItems(Item first, Item second);
}