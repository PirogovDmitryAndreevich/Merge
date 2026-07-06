using UnityEngine;
using UnityEngine.UI;

public class AttackItem : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Slider _healthBar;

    private IPoolService _pool;
    private int _health;

    public bool IsInitialized { get; private set; }
    public bool IsDestroy { get; private set; }
    public ItemData ItemData { get; private set; }

    public void Construct(IPoolService pool)
    {
        _pool = pool;
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

        if (_health <= 0)
        {
            IsDestroy = true;
            Hide();
        }
    }
}