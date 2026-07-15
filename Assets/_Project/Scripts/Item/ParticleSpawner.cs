using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    private IPoolService _pool;

    public void Construct(IPoolService pool) => 
        _pool = pool;

    public void PlayDamage(Vector2 position)
    {
        DamageEffect damage = _pool.Get<DamageEffect>();
        damage.Play(position);
    }    

    public void PlayDeath(Vector2 position)
    {
        DeathEffect death = _pool.Get<DeathEffect>();
        death.Play(position);
    }

    public void PlayClick(Vector2 position)
    {
        ClickEffect click = _pool.Get<ClickEffect>();
        click.Play(position);
    }

    public void PlayDamage(int profit, Vector2 position)
    {
        ValueEffect effect = _pool.Get<ValueEffect>();
        effect.Play(profit, position, ValueType.Damage);
    }
}