public interface IStaticData : IService
{
    GameData GameData { get; }
    ItemData ForItem(int id);
    void Load();
}