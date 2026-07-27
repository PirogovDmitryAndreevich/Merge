using UnityEngine;
using UnityEngine.UI;

public class ReviewButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private IReviewHandler _reviewHandler;

    private void OnDestroy()
    {
        _reviewHandler.ReviewAvailable -= ShowButton;
        _reviewHandler.ReviewNotAvailable -= HideButton;
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    public void Construct(IReviewHandler reviewHandler)
    {
        _reviewHandler = reviewHandler;

        _reviewHandler.ReviewAvailable += ShowButton;
        _reviewHandler.ReviewNotAvailable += HideButton;
        _button.onClick.AddListener(OnButtonClicked);

        HideButton();
    }

    private void ShowButton() => 
        gameObject.SetActive(true);

    private void HideButton() =>
        gameObject.SetActive(false);

    private void OnButtonClicked() =>
        _reviewHandler.ShowReview();
}
