using System;

public class SpawnButtonService : ISpawnButtonService
{
    private readonly IItemSpawnService _itemSpawn;
    private readonly IProgressService _progressService;
    private readonly IStaticData _staticData;
    private readonly ICursorHandler _cursorHandler;

    public ItemData CurrentItem { get; private set; }
    public ItemData BonusItem { get; private set; }
    public bool HaveCoinsForBuyItem =>
        _progressService.Progress.Player.Coins >= CurrentItem.Price;

    public event Action<bool> ViewChanged;
    public event Action ItemChanged;

    public SpawnButtonService(IItemSpawnService itemSpawn, IProgressService progressService,
        IStaticData staticData, ICursorHandler cursorHandler)
    {
        _itemSpawn = itemSpawn;
        _progressService = progressService;
        _staticData = staticData;
        _cursorHandler = cursorHandler;
    }

    public void CheckView()
    {
        if (!HaveCoinsForBuyItem)
            _cursorHandler.ShowMaxItem();

        ViewChanged?.Invoke(HaveCoinsForBuyItem);
    }

    public void Spawn()
    {
        _itemSpawn.Spawn(CurrentItem);

        if (_cursorHandler.IsButtonCursorEnable)
            _cursorHandler.HideCursor();
    }

    public void BonusSpawn() => 
        _itemSpawn.Spawn(BonusItem);

    public void UpdateItemData()
    {
        CurrentItem = _staticData.
            ForItem(_progressService.Progress.Items.SpawnItemID);

        BonusItem = _staticData.ForItem(CurrentItem.ID + 1);

        CheckView();

        ItemChanged?.Invoke();
    }
}