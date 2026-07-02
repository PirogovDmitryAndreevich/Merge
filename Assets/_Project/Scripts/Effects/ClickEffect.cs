
public class ClickEffect : Effect
{
    protected override void ReturnToPool()
    {
        _pool.Return(this);
    }
}
