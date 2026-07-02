using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{
    [SerializeField] private Image _icon;

    private IProgressService _progress;
    private int _id;

    private void OnDestroy() => 
        _progress.Progress.Items.NewItemOpened -= OnOpen;

    public void Construct(Sprite icon, IProgressService progressService, int id)
    {
        _progress = progressService;
        _id = id;

        _icon.sprite = icon;

        if (_progress.Progress.Items.OpenedItems.Contains(_id))
            OpenView();
        else
            CloseView();

        _progress.Progress.Items.NewItemOpened += OnOpen;
    }

    private void OnOpen(int id)
    {
        if (id == _id)
            OpenView();
    }

    private void CloseView() => 
        _icon.color = Color.black;

    private void OpenView() => 
        _icon.color = Color.white;
}