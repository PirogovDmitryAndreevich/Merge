using System.Collections.Generic;
using UnityEngine;

public class ItemSettings : MonoBehaviour
{
    private const string ItemsShapePath = "Items/ItemShape";
    private List<Sprite> sprites = new List<Sprite>();

    public void Load()
    {
        Sprite[] loadedSprites = Resources.LoadAll<Sprite>(ItemsShapePath);

        sprites.AddRange(loadedSprites);
    }
}
