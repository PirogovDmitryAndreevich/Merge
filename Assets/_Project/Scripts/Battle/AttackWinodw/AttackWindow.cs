using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWindow : MonoBehaviour
{
    private const float ShootDelay = 1.0f;

    [SerializeField] private ItemsFrame _enemyFrame;
    [SerializeField] private ItemsFrame _playerFrames;
    private IBattleService _battleService;

    private List<AttackItem> _playerItems;
    private List<AttackItem> _enemyItems;

    private void Start()
    {
        Hide();
    }

    public void Construct(IBattleService battleService, IPoolService poolService)
    {
        _battleService = battleService;
        _enemyFrame.Construct(poolService);
        _playerFrames.Construct(poolService);
    }

    public void Show()
    {
        _enemyItems = _enemyFrame.Initialize(_battleService.EnemyItems.ToArray());
        _playerItems = _playerFrames.Initialize(_battleService.SelectedItems.ToArray());

        gameObject.SetActive(true);

        StartCoroutine(StartBattle());
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator StartBattle()
    {
        yield return null;

        bool battleContinues = true;

        while (battleContinues)
        {
            // ’од игрока - все живые игроки стрел€ют по врагам
            yield return StartCoroutine(PlayerTurn());

            // ѕровер€ем, остались ли живые враги
            if (!HasAliveItems(_enemyItems))
            {
                Debug.Log("Player wins!");
                battleContinues = false;
                break;
            }

            // ’од врага - все живые враги стрел€ют по игрокам
            yield return StartCoroutine(EnemyTurn());

            // ѕровер€ем, остались ли живые игроки
            if (!HasAliveItems(_playerItems))
            {
                Debug.Log("Enemy wins!");
                battleContinues = false;
                break;
            }
        }

        // Ѕитва окончена
        yield return new WaitForSeconds(0.5f);
        Hide();
    }

    private IEnumerator PlayerTurn()
    {
        // ѕолучаем всех живых игроков
        List<AttackItem> alivePlayers = GetAliveItems(_playerItems);

        for (int i = 0; i < alivePlayers.Count; i++)
        {
            AttackItem player = alivePlayers[i];

            // Ќаходим цель дл€ этого игрока (первый живой враг)
            AttackItem target = GetFirstAliveTarget(_enemyItems);

            if (target != null)
            {
                player.Shoot(target);
                // ѕосле выстрела примен€ем урон к цели
                target.ApplyDamage();

                // ѕровер€ем, не уничтожена ли цель после урона
                if (target.IsDestroy)
                {
                    Debug.Log($"Enemy {target.name} destroyed!");
                }
            }

            yield return new WaitForSeconds(ShootDelay);
        }
    }

    private IEnumerator EnemyTurn()
    {
        // ѕолучаем всех живых врагов
        List<AttackItem> aliveEnemies = GetAliveItems(_enemyItems);

        for (int i = 0; i < aliveEnemies.Count; i++)
        {
            AttackItem enemy = aliveEnemies[i];

            // Ќаходим цель дл€ этого врага (первый живой игрок)
            AttackItem target = GetFirstAliveTarget(_playerItems);

            if (target != null)
            {
                enemy.Shoot(target);
                // ѕосле выстрела примен€ем урон к цели
                target.ApplyDamage();

                // ѕровер€ем, не уничтожена ли цель после урона
                if (target.IsDestroy)
                {
                    Debug.Log($"Player {target.name} destroyed!");
                }
            }

            yield return new WaitForSeconds(ShootDelay);
        }
    }

    // ¬спомогательные методы
    private bool HasAliveItems(List<AttackItem> items)
    {
        foreach (AttackItem item in items)
        {
            if (!item.IsDestroy)
                return true;
        }
        return false;
    }

    private List<AttackItem> GetAliveItems(List<AttackItem> items)
    {
        List<AttackItem> alive = new List<AttackItem>();
        foreach (AttackItem item in items)
        {
            if (!item.IsDestroy)
                alive.Add(item);
        }
        return alive;
    }

    private AttackItem GetFirstAliveTarget(List<AttackItem> items)
    {
        foreach (AttackItem item in items)
        {
            if (!item.IsDestroy)
                return item;
        }
        return null;
    }
}
