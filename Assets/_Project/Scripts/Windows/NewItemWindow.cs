using UnityEngine;
using UnityEngine.UI;

public class NewItemWindow : MonoBehaviour
{
    [SerializeField] private Button _acceptButton;
    [SerializeField] private Image _itemIcon;

    private IMergeService _mergeService;

    private void OnDestroy()
    {
        _acceptButton.onClick.RemoveListener(Hide);
        _mergeService.NewMaxItemReached -= OnNewMaxItemReached;
    }

    public void Construct(IMergeService mergeService)
    {
        _mergeService = mergeService;
        _mergeService.NewMaxItemReached += OnNewMaxItemReached;
        _acceptButton.onClick.AddListener(Hide);

        Hide();
    }

    public void Show(Sprite itemIcon)
    {
        _itemIcon.sprite = itemIcon;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnNewMaxItemReached(ItemData itemData)
    {
        Show(itemData.Sprite);
    }
}
