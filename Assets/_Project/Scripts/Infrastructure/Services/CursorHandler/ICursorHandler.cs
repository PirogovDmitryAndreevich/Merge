using System;

public interface ICursorHandler : IService
{
    event Action<Item> ShowingItemCursor;
    event Action ShowingButtonCursor;
    event Action HidingCursor;

    bool IsButtonCursorEnable { get; }
    float NotActionTimer { get; set; }

    void HideCursor();
    void ShowButtonClick();
    void ShowMaxItem();
}