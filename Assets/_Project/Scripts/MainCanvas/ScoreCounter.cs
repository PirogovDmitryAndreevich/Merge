using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsText;
    private IProgressService _progress;

    private void OnDestroy() =>
        _progress.Progress.Player.ScoreChanged -= UpdateCounter;

    public void Construct(IProgressService progress)
    {
        _progress = progress;
        _progress.Progress.Player.ScoreChanged += UpdateCounter;
        UpdateCounter();
    }

    private void UpdateCounter() =>
        _coinsText.text = _progress.Progress.Player.Score.ToString();
}