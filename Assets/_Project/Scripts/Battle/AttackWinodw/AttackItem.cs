using UnityEngine;
using UnityEngine.UI;

public class AttackItem : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void Initialize(Sprite icon)
    {
        _image.sprite = icon;
    }
}