using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private CanvasGroup _group;

    public void SetAlpha(float value) =>
        _group.alpha = value;
}