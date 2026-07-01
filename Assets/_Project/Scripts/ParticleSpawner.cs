using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    private IPoolService _pool;

    public void Construct(IPoolService pool) => 
        _pool = pool;

    public void Play(int value, Vector2 position)
    {
        ProfitEffect effect = _pool.Get<ProfitEffect>();
        ClickEffect click = _pool.Get<ClickEffect>();

        click.Play(position);
        effect.Play(value, position);
    }
}