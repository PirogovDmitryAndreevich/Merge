public class BonusSpawnService : IBonusSpawnService
{
    private float _luck;
    private float _target = 100;

    public bool ShouldSpawn(ItemData data)
    {
        _luck += data.Luck;

        if (data.ID <= 3)
            return false;

        if (_luck >= _target)
        {
            _luck = 0;
            _target = UnityEngine.Random.Range(90f, 110f);
            return true;
        }

        return false;
    }
}