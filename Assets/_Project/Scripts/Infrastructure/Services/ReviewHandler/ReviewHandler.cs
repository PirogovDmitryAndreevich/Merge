using System;
using YG;

public class ReviewHandler : IReviewHandler
{
    private readonly ISpawnButtonService _spawnButtonService;

    public ReviewHandler(ISpawnButtonService spawnButtonService)
    {
        _spawnButtonService = spawnButtonService;
    }

    public event Action ReviewAvailable;
    public event Action ReviewNotAvailable;

    public void ShowReviewButton() =>
        ReviewAvailable?.Invoke();

    public void HideReviewButton() =>
        ReviewNotAvailable?.Invoke();

    public void UpdateData()
    {
#if UNITY_EDITOR
        YG2.reviewCanShow = true;
#endif
        if (YG2.reviewCanShow)
            ShowReviewButton();
        else
            HideReviewButton();
    }

    public void OnShowReview()
    {
        if (YG2.reviewCanShow)        
            ShowReview();        
    }

    public void ShowReview()
    {
        YG2.reviewCanShow = false;
        YG2.onReviewSent += ReviewSent;
        YG2.ReviewShow();
    }

    private void ReviewSent(bool sent)
    {
        YG2.onReviewSent -= ReviewSent;

        if (sent)        
            OnReviewSuccess();        
        else        
            OnReviewFailed();        

        ReviewNotAvailable.Invoke();
    }

    private void OnReviewSuccess()
    {
        for (int i = 0; i < 10; i++)        
            _spawnButtonService.Spawn();        
    }

    private void OnReviewFailed()
    {
        YG2.reviewCanShow = true;
    }
}