using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public abstract class Effect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;

    private IPoolService _pool;

    private void OnParticleSystemStopped() =>
        _pool.Return(this);

    public void Construct(IPoolService poolService) =>
        _pool = poolService;

    public void Play(Vector2 point)
    {
        transform.position = point;
        _particles.Play();
    }
}
