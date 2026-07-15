using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private LeaderboardYG _leaderboard;    
    [SerializeField] private Button _closeButton;
    [SerializeField] private CanvasGroup _canvasGroup;

    private IProgressService _progressService;

    public bool IsActive { get; private set; }

    private void OnDestroy()
    {
        _progressService.Progress.Player.ScoreChanged -= SetNewValueLB;
        _closeButton.onClick.AddListener(Hide);
    }

    private void Start() => 
        gameObject.SetActive(false);

    public void Construct(IProgressService progressService)
    {
        _progressService = progressService;

        _progressService.Progress.Player.ScoreChanged += SetNewValueLB;
        _closeButton.onClick.AddListener(Hide);

        SetNewValueLB();
        Hide();
    }

    public void Show()
    {
        IsActive = true;

        gameObject.SetActive(true);

        _canvasGroup.DOFade(1f, 0.5f).From(0f);
    }

    public void Hide()
    {
        IsActive = false;

        _canvasGroup.DOFade(0f, 0.5f).From(1f)
            .OnComplete(() => gameObject.SetActive(false));
    }

    private void SetNewValueLB()
    {
        _leaderboard.SetLeaderboard(_progressService.Progress.Player.Score);
        _leaderboard.UpdateLB();
    }
}