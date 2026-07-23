using UnityEngine;

public class CursorPointer : MonoBehaviour
{
    [SerializeField] private Transform _iconTransform;
    [SerializeField] private CanvasGroup _text;

    private ICursorHandler _cursorHandler;
    private Transform _buttonTransform;
    private CursorAnimation _cursorAnimation;
    private Coroutine _animation;

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
        _cursorAnimation = new CursorAnimation();

        _cursorHandler.ShowingItemCursor += OnShowMaxItem;
        _cursorHandler.ShowingButtonCursor += OnShowButton;
        _cursorHandler.HidingCursor += Hide;

        Hide();
    }

    private void OnShowMaxItem(Item item)
    {
        gameObject.SetActive(true);
        transform.position = item.transform.position;
        StartClickAnimation();
    }

    private void OnShowButton()
    {
        gameObject.SetActive(true);
        transform.position = _buttonTransform.position;
        StartClickAnimation();
    }

    private void Hide()
    {
        if (_animation != null)
            StopCoroutine(_animation);

        gameObject.SetActive(false);
    }

    private void StartClickAnimation()
    {
        _cursorAnimation.ClickAnimation(_iconTransform, _text);
    }
}
