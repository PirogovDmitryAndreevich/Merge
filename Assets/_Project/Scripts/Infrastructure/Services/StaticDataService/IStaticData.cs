public interface IStaticData : IService
{
    ItemData ForItem(int id);
    void Load();
}