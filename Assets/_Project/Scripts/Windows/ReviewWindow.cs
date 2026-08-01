using UnityEngine;
using UnityEngine.UI;

public class ReviewWindow : MonoBehaviour
{
    [SerializeField] private Button _acceptButton;
    [SerializeField] private Button _cancelButton;

    private IReviewHandler _reviewHandler;

    private void OnDestroy()
    {
        _acceptButton.onClick.RemoveListener(OnAcceptClicked);
        _cancelButton.onClick.RemoveListener(Hide);
    }

    public void Construct(IReviewHandler reviewHandler)
    {
        _reviewHandler = reviewHandler;

        _acceptButton.onClick.AddListener(OnAcceptClicked);
        _cancelButton.onClick.AddListener(Hide);

        gameObject.SetActive(false);
    }

    public void Show() => 
        gameObject.SetActive(true);

    private void Hide() => 
        gameObject.SetActive(false);

    private void OnAcceptClicked()
    {
        _reviewHandler.ShowReview();
        Hide();
    }
}