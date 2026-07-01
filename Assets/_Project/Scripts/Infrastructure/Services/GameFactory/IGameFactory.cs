public interface IGameFactory : IService
{
    MainCanvas CreateMainCanvas();
    Item CreateItem();
    ProfitEffect CreateProfit();
    ClickEffect CreateClickEffect();
    MergeEffect CreateMergeEffect();
}