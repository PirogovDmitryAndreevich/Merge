using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AttackItem : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Arrow _arrow;

    private IPoolService _pool;

    public bool IsInitialized { get; private set; }
    public bool IsDestroy { get; private set; }
    public ItemData ItemData { get; private set; }

    public void Construct(IPoolService pool)
    {
        _pool = pool;
    }

    public void Initialize(ItemData itemData)
    {
        ItemData = itemData;
        _image.sprite = ItemData.Sprite;
        IsInitialized = true;
        IsDestroy = false;
    }

    public void Show() =>
        gameObject.SetActive(true);

    public void Hide() =>
        gameObject.SetActive(false);

    public void ApplyDamage()
    {
        Debug.Log("ApplyDamage");
    }

    public void Shoot(AttackItem target) => 
        _arrow.Shoot(target);
}