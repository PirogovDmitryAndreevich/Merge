using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private ContactFilter2D _contactFilter;
    [SerializeField] private ItemID _id;
    [SerializeField] private ItemID _nextId;

    private readonly Collider2D[] _overlapResults = new Collider2D[8];
    private bool _isMerged;
    private Canvas _canvas;

    public ItemID ID => _id;

    public void Construct(Canvas canvas)
    {
        _canvas = canvas;
    }

    public void OnDrag(PointerEventData eventData) => 
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;

    public void OnEndDrag(PointerEventData eventData) => 
        TryMerge();

    private void TryMerge()
    {
        if (_isMerged)
            return;

        int count = _collider.OverlapCollider(_contactFilter, _overlapResults);

        for (int i = 0; i < count; i++)
        {
            if (_overlapResults[i].TryGetComponent(out Item other) == false)
                continue;

            if (other == this || other.ID != ID)
                continue;

            Merge(other);
            return;
        }
    }


    private void Merge(Item other)
    {
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
