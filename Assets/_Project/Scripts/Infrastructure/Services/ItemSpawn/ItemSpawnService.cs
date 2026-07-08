using DG.Tweening;
using UnityEngine;

public class ItemSpawnService : IItemSpawnService
{
    private readonly IStaticData _staticData;
    private readonly IProgressService _progressService;
    private readonly IItemsPoolService _itemsPool;
    private readonly IBonusSpawnService _bonusSpawnService;

    private Vector3[] _corners = new Vector3[4];
    private ItemData _currentItemData;
    private SpawnButton _spawnButton;

    public ItemSpawnService(IStaticData staticData,
        IProgressService progressService, IItemsPoolService pool,
        IBonusSpawnService bonusSpawnService)
    {
        _staticData = staticData;
        _progressService = progressService;
        _itemsPool = pool;
        _bonusSpawnService = bonusSpawnService;
    }

    public void SetSpawnFrameAndButton(RectTransform frame, SpawnButton spawnButton)
    {
        frame.GetWorldCorners(_corners);
        _spawnButton = spawnButton;

        UpdateItemData();
    }

    public void UpdateItemData()
    {
        _currentItemData = _staticData.
            ForItem(_progressService.Progress.Items.SpawnItemID);

        _spawnButton.Initialize(_currentItemData.Sprite, _currentItemData.Price);
    }

    public void Spawn()
    {
        Item item = _itemsPool.GetItem<Item>();

        ItemSaveData itemSaveData;

        if (item.SaveData == null)
            itemSaveData = new ItemSaveData();
        else
            itemSaveData = item.SaveData;

        item.Initialize(_currentItemData, itemSaveData);
        
        MoveItem(item);

        _progressService.Progress.Items.Items.Add(item.SaveData);
        _progressService.Progress.Player.SpendCoins(_currentItemData.Price);

        if (_bonusSpawnService.ShouldSpawn(item.ItemData))
        {
            BonusItem bonus = _itemsPool.GetItem<BonusItem>();
            MoveItem(bonus);
        }
    }

    private void MoveItem(IMoveableItem itemTransform)
    {
        itemTransform.RectTransform.position = _spawnButton.transform.position;

        Vector3 min = _corners[0];
        Vector3 max = _corners[2];

        Vector2 targetPosition = new(
            Random.Range(min.x, max.x),
            Random.Range(min.y, max.y));

        itemTransform.RectTransform.DOMove(targetPosition, 0.5f)
            .SetUpdate(true)
            .SetEase(Ease.OutCirc)
            .Play()
            .OnComplete(itemTransform.SavePosition);
    }  
}
