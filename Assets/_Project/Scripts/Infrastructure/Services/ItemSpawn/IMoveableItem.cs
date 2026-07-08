using UnityEngine;

public interface IMoveableItem
{
    RectTransform RectTransform { get; }
    void SavePosition();
}