using UnityEngine;

public class CursorPointer : MonoBehaviour
{
    private ICursorHandler _cursorHandler;
    private Transform _buttonTransform;

    private void OnDestroy()
    {
        _cursorHandler.ShowingItemCursor -= OnShowMaxItem;
        _cursorHandler.ShowingButtonCursor -= OnShowButton;
        _cursorHandler.HidingCursor -= Hide;
    }

    public void Construct(ICursorHandler cursorHandler, Transform spawnButton)
    {
        _cursorHandler = cursorHandler;
        _buttonTransform = spawnButton;

        _cursorHandler.ShowingItemCursor += OnShowMaxItem;
        _cursorHandler.ShowingButtonCursor += OnShowButton;
        _cursorHandler.HidingCursor += Hide;
    }

    private void OnShowMaxItem(Item item)
    {
        gameObject.SetActive(true);
        transform.position = item.transform.position;
    }

    private void OnShowButton()
    {
        gameObject.SetActive(true);
        transform.position = _buttonTransform.position;
    }

    private void Hide() => 
        gameObject.SetActive(false);
}