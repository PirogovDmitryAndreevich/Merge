using System.Collections.Generic;
using UnityEngine;

public class ItemsFrame : MonoBehaviour
{
    [SerializeField] private AttackItem[] _images;

    private List<AttackItem> _initializedItems;

    public void Construct(IPoolService poolService, UIImpactShake impactShake, AudioPlayer audioPlayer)
    {
        foreach(var item in _images)
            item.Construct(poolService, impactShake, audioPlayer);
    }

    public List<AttackItem> Initialize(ItemData[] items)
    {
        _initializedItems = new();

        for (int i = 0; i < _images.Length; i++)
        {
            if (i < items.Length)
            {
                _images[i].Show();
                _images[i].Initialize(items[i]);
                _initializedItems.Add(_images[i]);
            }
            else
            {
                _images[i].Hide();
            }
        }

        return _initializedItems;
    }
}
