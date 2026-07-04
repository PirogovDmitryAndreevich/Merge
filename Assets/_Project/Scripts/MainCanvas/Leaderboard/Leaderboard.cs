using UnityEngine;
using YG;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private LeaderboardYG _leaderboard;
    private IProgressService _progressService;

    private void Start() =>
        SetNewValueLB();

    private void OnDestroy() =>
        _progressService.Progress.Player.ScoreChanged -= SetNewValueLB;

    public void Construct(IProgressService progressService)
    {
        _progressService = progressService;
        _progressService.Progress.Player.ScoreChanged += SetNewValueLB;

        SetNewValueLB();
    }

    private void SetNewValueLB()
    {
        _leaderboard.SetLeaderboard(_progressService.Progress.Player.Score);
        _leaderboard.UpdateLB();
    }
}