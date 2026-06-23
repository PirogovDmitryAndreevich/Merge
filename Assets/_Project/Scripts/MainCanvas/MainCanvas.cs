using DG.Tweening;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private SpawnButton _spawnButton;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _spawnFrame;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private Transform _itemParent;

    private Vector3[] _corners = new Vector3[4];
    private Vector2 _buttonPosition;

    private void OnDestroy()
    {
        _spawnButton.ButtonClicked -= OnButtonClicked;
    }

    public void Construct()
    {
        _spawnFrame.GetWorldCorners(_corners);

        _buttonPosition =
            _spawnButton.GetComponent<RectTransform>().position;

        _spawnButton.ButtonClicked += OnButtonClicked;
    }

    private void OnButtonClicked()
    {
        Item item =
            Instantiate(_itemPrefab, _itemParent).GetComponent<Item>();

        item.Construct(_canvas);
        item.transform.position = _buttonPosition;

        Vector3 min = _corners[0];
        Vector3 max = _corners[2];

        Vector2 targetPosition = new(
            Random.Range(min.x, max.x),
            Random.Range(min.y, max.y));

        item.transform.DOMove(targetPosition, 0.5f)
            .SetUpdate(true)
            .SetEase(Ease.OutCirc)
            .Play();
    }
}
