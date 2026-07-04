using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Utils.LB;

public class StartBattleWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Button _acceptButton;
    [SerializeField] private Button _cancelButton;

    private LBData _lbData;
    private SelectWindow _selectWindow;

    private void OnDestroy()
    {
        _acceptButton.onClick.RemoveListener(OnAccept);
        _cancelButton.onClick.RemoveListener(Hide);
        YG2.onGetLeaderboard -= OnGetLeaderboard;
    }

    private void Start()
    {
        _acceptButton.onClick.AddListener(OnAccept);
        _cancelButton.onClick.AddListener(Hide);
        
        Hide();
    }

    public void Construct(SelectWindow selectWindow)
    {
        _selectWindow = selectWindow;
        YG2.onGetLeaderboard += OnGetLeaderboard;
    }

    public void Show()
    {
        if (_lbData == null || _lbData.players == null || _lbData.players.Length == 0)
        {
            if (_lbData == null)
                Debug.LogWarning("Leaderboard data is null");
            else if (_lbData.players == null)
                Debug.LogWarning("Leaderboard players array is null");
            else if (_lbData.players.Length == 0)
                Debug.LogWarning("Leaderboard players array is empty");

            return;
        }

        int randomIndex = Random.Range(0, _lbData.players.Length);
        _name.text = _lbData.players[randomIndex].name;

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnAccept()
    {
        _selectWindow.Show();
        Hide();
    }

    private void OnGetLeaderboard(LBData data)
    {
        _lbData = data;
    }
}
