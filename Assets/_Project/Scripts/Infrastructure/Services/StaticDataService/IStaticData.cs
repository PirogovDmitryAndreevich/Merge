public interface IStaticData : IService
{
    ItemData ForItem(ItemID id);
    void Load();
}