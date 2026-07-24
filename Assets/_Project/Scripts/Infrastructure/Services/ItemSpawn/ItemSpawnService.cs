using DG.Tweening;
using UnityEngine;

public class ItemSpawnService : IItemSpawnService
{
    private readonly IProgressService _progressService;
    private readonly IItemsPoolService _itemsPool;
    private readonly IBonusSpawnService _bonusSpawnService;
    private Vector3[] _corners = new Vector3[4];
    private SpawnButton _spawnButton;

    public ItemSpawnService(IProgressService progressService,
        IItemsPoolService pool,
        IBonusSpawnService bonusSpawnService)
    {
        _progressService = progressService;
        _itemsPool = pool;
        _bonusSpawnService = bonusSpawnService;
    }

    public void SetSpawnFrameAndButton(RectTransform frame, SpawnButton spawnButton)
    {
        frame.GetWorldCorners(_corners);
        _spawnButton = spawnButton;
    }

    public void Spawn(ItemData itemData)
    {
        Item item = _itemsPool.GetItem<Item>();

        ItemSaveData itemSaveData;

        if (item.SaveData == null)
            itemSaveData = new ItemSaveData();
        else
            itemSaveData = item.SaveData;

        item.Initialize(itemData, itemSaveData);
        
        MoveItem(item);

        _progressService.Progress.Items.Items.Add(item.SaveData);
        _progressService.Progress.Player.SpendCoins(itemData.Price);

        if (_bonusSpawnService.ShouldSpawn(item.ItemData))        
            SpawnBonusItem();        
    }

    public void SpawnBonusItem()
    {
        BonusItem bonus = _itemsPool.GetItem<BonusItem>();
        bonus.Init();
        MoveItem(bonus);
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
