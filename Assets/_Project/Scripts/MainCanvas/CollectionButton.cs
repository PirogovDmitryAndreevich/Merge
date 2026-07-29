using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _text;

    private CollectionWindow _collectionWindow;
    private IProgressService _progress;
    private int _allItems;

    private void OnDestroy()
    {
        _progress.Progress.Items.NewItemOpened -= UpdateText;
        _button.onClick.RemoveListener(ShowCollection);
    }

    public void Construct(CollectionWindow collectionWindow, IProgressService progress, int itemsCount)
    {
        _collectionWindow = collectionWindow;
        _progress = progress;
        _allItems = itemsCount;

        _progress.Progress.Items.NewItemOpened += UpdateText;
        _button.onClick.AddListener(ShowCollection);

        UpdateText(_allItems);
    }

    private void ShowCollection() =>
        _collectionWindow.Show();

    private void UpdateText(int _)
    {
        _text.text = $"{_progress.Progress.Items.OpenedItems.Count} / {_allItems}";
    }
}