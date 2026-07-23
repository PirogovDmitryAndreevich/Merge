using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private IBattleService _battleService;

    private void OnDestroy() => 
        _button.onClick.RemoveListener(OnClick);

    public void Construct(IBattleService battleService)
    {
        _battleService = battleService;
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick() => 
        _battleService.Start(AttackWindowType.PlayerAttack);
}