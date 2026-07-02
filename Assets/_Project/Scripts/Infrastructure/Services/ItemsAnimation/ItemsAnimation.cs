using UnityEngine;

public class ItemsAnimation : IItemsAnimation
{
    private readonly IItemsPoolService _itemsPoolService;
    private GameData _gameData;
    private float _globalTime;
    private Vector3 _cachedRotation;

    public ItemsAnimation(IItemsPoolService itemsPoolService, IStaticData staticData)
    {
        _itemsPoolService = itemsPoolService;
        _gameData = staticData.GameData;
    }

    public void Update()
    {
        _globalTime += Time.deltaTime * _gameData.AnimationItemsSpeed;
        float angle = Mathf.Sin(_globalTime) * _gameData.AnimationItemsAmplitude;

        _cachedRotation.z = angle;

        foreach (var item in _itemsPoolService.ItemsOnScene)
            item.RectTransform.localEulerAngles = _cachedRotation;
    }
}