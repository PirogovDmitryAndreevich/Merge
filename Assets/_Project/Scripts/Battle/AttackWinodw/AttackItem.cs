using UnityEngine;
using UnityEngine.UI;

public class AttackItem : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private ParticleSpawner _particleSpawner;
    [SerializeField] private AudioClip _damageSound;
    [SerializeField] private AudioClip _deathSound;

    private int _health;
    private UIImpactShake _impactShake;
    private AudioPlayer _audioPlayer;

    public bool IsInitialized { get; private set; }
    public bool IsDestroy { get; private set; }
    public ItemData ItemData { get; private set; }

    public void Construct(IPoolService pool, UIImpactShake impactShake, AudioPlayer audioPlayer)
    {
        _particleSpawner.Construct(pool);
        _impactShake = impactShake;
        _audioPlayer = audioPlayer;
    }

    public void Initialize(ItemData itemData)
    {
        ItemData = itemData;
        _health = ItemData.Health;
        _image.sprite = ItemData.Sprite;

        _healthBar.minValue = 0f;
        _healthBar.maxValue = 1f;
        _healthBar.value = 1f;

        IsInitialized = true;
        IsDestroy = false;
    }

    public void Show() =>
        gameObject.SetActive(true);

    public void Hide() =>
        gameObject.SetActive(false);

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        _health = Mathf.Max(_health, 0);

        _healthBar.value = (float)_health / ItemData.Health;

        _audioPlayer.PlaySound(_damageSound);
        _impactShake.Play();
        _particleSpawner.PlayDamage(transform.position);
        _particleSpawner.PlayDamage(damage, transform.position);

        if (_health <= 0)
        {
            _audioPlayer.PlaySound(_deathSound);
            _particleSpawner.PlayDeath(transform.position);
            IsDestroy = true;
            Hide();
        }
    }
}