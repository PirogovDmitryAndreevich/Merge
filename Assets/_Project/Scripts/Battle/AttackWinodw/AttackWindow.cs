using UnityEngine;

public class AttackWindow : MonoBehaviour
{
    [SerializeField] private ItemsFrame _enemyFrame;
    [SerializeField] private ItemsFrame _playerFrames;
    private IBattleService _battleService;

    private void Start()
    {
        Hide();
    }

    public void Construct(IBattleService battleService)
    {
        _battleService = battleService;
    }

    public void Show()
    {
        _enemyFrame.Initialize(_battleService.EnemyItems.ToArray());
        _playerFrames.Initialize(_battleService.SelectedItems.ToArray());

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
