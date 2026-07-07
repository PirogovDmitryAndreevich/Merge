public class DeathEffect : Effect
{
    protected override void ReturnToPool()
    {
        _pool.Return(this);
    }
}
