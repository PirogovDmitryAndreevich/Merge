public class DamageEffect : Effect
{
    protected override void ReturnToPool()
    {
        _pool.Return(this);
    }
}
