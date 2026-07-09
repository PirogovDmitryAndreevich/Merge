using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour
{    
    [SerializeField] private Button _button;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _price;

    private IProgressService _progress;
    private int _currentPrice;

    public event Action ButtonClicked;

    private void OnEnable() => 
        _button.onClick.AddListener(OnButtonPressed);

    private void OnDisable() => 
        _button.onClick.RemoveListener(OnButtonPressed);

    public void Construct(IProgressService progressService)
    {
        _progress = progressService;
        _progress.Progress.Player.CoinsChanged += ChangeView;

        ChangeView();
    }

    public void Initialize(Sprite icon, int price)
    {
        _icon.sprite = icon;
        _price.text = price.ToString();
        _currentPrice = price;
    }

    private void OnButtonPressed() => 
        ButtonClicked?.Invoke();

    private void ChangeView()
    {
        if (_progress.Progress.Player.Coins >= _currentPrice)
        {
        _price.color = Color.white;
        _button.interactable = true;
        }
        else
        {
            _price.color = Color.red;
            _button.interactable = false;
        } 
    }
}
