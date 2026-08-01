using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _textSlider;
    [SerializeField] private TMP_Text _counter;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _alert;
    [SerializeField] private Button _button;

    private IProgressService _progress;
    private IAwardHandler _awardHandler;
    private PlayerSaveData _data;
    private AwardWindow _awardWindow;
    private bool _isAvailableView;

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
        _data.UnspentScoreChanged -= SetView;
    }

    public void Construct(IProgressService progress, IAwardHandler awardHandler, AwardWindow awardWindow)
    {
        _progress = progress;
        _awardHandler = awardHandler;
        _data = _progress.Progress.Player;
        _awardWindow = awardWindow;

        _button.onClick.AddListener(OnButtonClicked);
        _data.UnspentScoreChanged += SetView;

        _slider.minValue = 0f;
        _slider.maxValue = 1f;

        _isAvailableView = !_awardHandler.HaveAvailable;
        SetView();
    }

    private void OnButtonClicked()
    {
        _awardWindow.Show();
        _awardHandler.GiveOutReward();
    }

    private void SetView()
    {
        _awardHandler.CheckReached();

        if (_isAvailableView != _awardHandler.HaveAvailable)
        {
            _isAvailableView = _awardHandler.HaveAvailable;
            _alert.gameObject.SetActive(_isAvailableView);
            _button.gameObject.SetActive(_isAvailableView);
            _button.interactable = _isAvailableView;
        }

        if (_isAvailableView)        
            _counter.text = _data.AvailableAwards.ToString();        

        _textSlider.text = $"{_data.UnspentScores} / {_data.TargetScoreToReward}";
        float progress = Mathf.Clamp01((float)_data.UnspentScores / _data.TargetScoreToReward);
        _slider.value = progress;
    }
}