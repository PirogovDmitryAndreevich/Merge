using UnityEngine;

public class CursorPointer : MonoBehaviour
{
    [SerializeField] private Transform _iconTransform;
    [SerializeField] private CanvasGroup _text;

    private ICursorHandler _cursorHandler;
    private Transform _buttonTransform;
    private CursorAnimation _cursorAnimation;

    private bool _isClickAnimation;
    //private bool _isMergeAnimation;

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
        _isClickAnimation = true;
        gameObject.SetActive(true);
        transform.position = item.transform.position;
        StartClickAnimation();
    }

    private void OnShowButton()
    {
        _isClickAnimation = true;
        gameObject.SetActive(true);
        transform.position = _buttonTransform.position;
        StartClickAnimation();
    }

    private void Hide()
    {
        gameObject.SetActive(false);

        if(_isClickAnimation)
        {
            _isClickAnimation = false;
            _cursorAnimation.StopClickAnimation();
        }
    }

    private void StartClickAnimation()
    {
        _cursorAnimation.ClickAnimation(_iconTransform, _text);
    }
}
