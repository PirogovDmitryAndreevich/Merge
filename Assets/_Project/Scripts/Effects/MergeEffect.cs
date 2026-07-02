public class MergeEffect : Effect
{
    protected override void ReturnToPool()
    {
        _pool.Return(this);
    }
}
