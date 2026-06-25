using UnityEngine;

public interface IGameFactory : IService
{
    MainCanvas CreateMainCanvas();
    Item CreateItem();
    ProfitEffect CreateProfit();
}