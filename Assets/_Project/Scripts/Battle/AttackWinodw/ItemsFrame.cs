using UnityEngine;

public class ItemsFrame : MonoBehaviour 
{
    [SerializeField] private AttackItem[] _images; 

    public void Initialize(ItemData[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            _images[i].Initialize(items[i].Sprite);
        }
    }
}
