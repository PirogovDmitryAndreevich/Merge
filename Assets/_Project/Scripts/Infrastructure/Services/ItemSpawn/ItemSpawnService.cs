using DG.Tweening;
using UnityEngine;

public class ItemSpawnService : IItemSpawnService
{
    private readonly IGameFactory _gameFactory;
    private readonly IStaticData _staticData;
    private readonly IProgressService _progressService;
    private readonly IPoolService _pool;
    private Vector3[] _corners = new Vector3[4];

    public ItemSpawnService(IGameFactory gameFactory, IStaticData staticData,
        IProgressService progressService, IPoolService pool)
    {
        _gameFactory = gameFactory;
        _staticData = staticData;
        _progressService = progressService;
        _pool = pool;
    }

    public void SetSpawnFrame(RectTransform frame) =>
        frame.GetWorldCorners(_corners);

    public void Spawn(Vector3 startPosition)
    {
        Item item = _pool.Get<Item>();

        ItemData itemData = _staticData.ForItem(ItemID.Item_1); // Get Item ID

        ItemSaveData itemSaveData;

        if (item.SaveData == null)
            itemSaveData = new ItemSaveData();
        else
            itemSaveData = item.SaveData;

        item.Initialize(itemData, itemSaveData);
        item.transform.position = startPosition;

        Vector3 min = _corners[0];
        Vector3 max = _corners[2];

        Vector2 targetPosition = new(
            Random.Range(min.x, max.x),
            Random.Range(min.y, max.y));

        item.transform.DOMove(targetPosition, 0.5f)
            .SetUpdate(true)
            .SetEase(Ease.OutCirc)
            .Play()
            .OnComplete(() => SetItemPosition(item));

        _progressService.Progress.Items.Items.Add(item.SaveData);
    }

    private void SetItemPosition(Item item) =>
        item.SavePosition();
}