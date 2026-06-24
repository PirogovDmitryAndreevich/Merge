using DG.Tweening;
using UnityEngine;

public class ItemSpawnService : IItemSpawnService
{
    private readonly IGameFactory _gameFactory;
    private readonly IStaticData _staticData;
    private Vector3[] _corners = new Vector3[4];
    private Vector2 _startPosition;

    public ItemSpawnService(IGameFactory gameFactory, IStaticData staticData)
    {
        _gameFactory = gameFactory;
        _staticData = staticData;
    }

    public void SetSpawnFrame(RectTransform frame, Vector2 startSpawnPoint)
    {
        frame.GetWorldCorners(_corners);
        _startPosition = startSpawnPoint;
    }

    public void Spawn()
    {
        Item item = _gameFactory.CreateItem();

        ItemData itemData = _staticData.ForItem(ItemID.Item_1); // Get Item ID

        item.Initialize(itemData);
        item.transform.position = _startPosition;

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