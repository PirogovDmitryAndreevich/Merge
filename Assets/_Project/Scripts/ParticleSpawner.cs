using System;
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

    public void PlayValue(int profit, Vector2 position)
    {
        ProfitEffect effect = _pool.Get<ProfitEffect>();
        effect.Play(profit, position);
    }
}