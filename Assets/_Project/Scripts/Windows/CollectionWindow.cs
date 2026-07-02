using UnityEngine;
using UnityEngine.UI;

public class CollectionWindow : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Transform _content;

    public Transform Content => _content;

    private void OnDestroy() => 
        _closeButton.onClick.RemoveListener(Hide);

    private void Start()
    {
        _closeButton.onClick.AddListener(Hide);

        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}