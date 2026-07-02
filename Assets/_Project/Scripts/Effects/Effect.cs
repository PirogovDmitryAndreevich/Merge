using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public abstract class Effect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;

    protected IPoolService _pool;

    private void OnParticleSystemStopped() => 
        ReturnToPool();

    public void Construct(IPoolService poolService)
    {
        _pool = poolService;

        var main = _particles.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    public void Play(Vector2 point)
    {
        transform.position = point;
        _particles.Play();
    }

    protected abstract void ReturnToPool();
}
