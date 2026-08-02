using System.Collections.Generic;

public class HintService : IHintService
{
    private const float ActionTimer = 10f;

    private readonly IItemsPoolService _itemsPool;
    private readonly ICursorHandler _cursorHandler;
    private readonly ISpawnButtonService _spawnButtonService;
    private float _timer;

    public HintService(IItemsPoolService itemsPool, ICursorHandler cursorHandler,
        ISpawnButtonService spawnButtonService)
    {
        _itemsPool = itemsPool;
        _cursorHandler = cursorHandler;
        _spawnButtonService = spawnButtonService;
    }

    public void UpdateTimer(float deltaTime)
    {
        _timer -= deltaTime;

        if (_timer <= 0)
        {
            ShowHint();
            ResetTimer();
        }
    }

    public void ResetTimer() =>
        _timer = ActionTimer;

    private void ShowHint()
    {
        Dictionary<int, Item> itemsById = new Dictionary<int, Item>();

        foreach (Item item in _itemsPool.ItemsOnScene)
        {
            if (itemsById.TryGetValue(item.ID, out Item duplicateItem))
            {
                _cursorHandler.ShowMergeItems(
                    duplicateItem,
                    item);

                return;
            }

            itemsById.Add(item.ID, item);
        }

        if (_spawnButtonService.HaveCoinsForBuyItem)
        {
            _cursorHandler.ShowButtonClick();
            return;
        }
        else
        {
            _cursorHandler.ShowMaxItem();
        }
    }
}