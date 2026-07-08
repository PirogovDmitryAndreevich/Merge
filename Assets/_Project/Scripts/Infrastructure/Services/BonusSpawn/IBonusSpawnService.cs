public interface IBonusSpawnService : IService
{
    bool ShouldSpawn(ItemData data);
}