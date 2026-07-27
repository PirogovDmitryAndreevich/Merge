using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonScaleOnInteract : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    [Header("Scale Settings")]
    [SerializeField] private float _hoverScale = 1.04f;
    [SerializeField] private float _pressedScale = 0.97f;

    [Header("Animation")]
    [SerializeField] private float _duration = 0.1f;
    [SerializeField] private Ease _ease = Ease.OutQuad;

    private RectTransform _rectTransform;
    private Tween _scaleTween;
    private Vector3 _initialScale;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _initialScale = _rectTransform.localScale;
    }

    private void OnDisable()
    {
        _scaleTween?.Kill();
    }    

    public void OnPointerEnter(PointerEventData eventData)
    {
        ScaleTo(_hoverScale);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ScaleTo(1f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ScaleTo(_pressedScale);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ScaleTo(_hoverScale);
    }

    private void ScaleTo(float scale)
    {
        _scaleTween?.Kill();
        _scaleTween = _rectTransform
            .DOScale(_initialScale * scale, _duration)
            .SetUpdate(true)
            .SetEase(_ease)
            .Play();
    }
}
