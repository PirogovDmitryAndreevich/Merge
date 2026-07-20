using System;

public class SpawnButtonService : ISpawnButtonService
{
    private readonly IItemSpawnService _itemSpawn;
    private readonly IProgressService _progressService;
    private readonly IStaticData _staticData;
    private readonly ICursorHandler _cursorHandler;

    public ItemData CurrentItem { get; private set; }
    public bool IsActive { get; private set; }

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
        IsActive =
            _progressService.Progress.Player.Coins >= CurrentItem.Price;

        if (!IsActive)
            _cursorHandler.ShowMaxItem();

        ViewChanged?.Invoke(IsActive);
    }

    public void Spawn()
    {
        _itemSpawn.Spawn(CurrentItem);

        if (_cursorHandler.IsButtonCursorEnable)
            _cursorHandler.HideCursor();
    }

    public void UpdateItemData()
    {
        CurrentItem = _staticData.
            ForItem(_progressService.Progress.Items.SpawnItemID);

        CheckView();

        ItemChanged?.Invoke();
    }
}