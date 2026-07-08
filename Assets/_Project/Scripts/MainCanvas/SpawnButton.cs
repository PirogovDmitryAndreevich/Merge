using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour
{    
    [SerializeField] private Button _button;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _price;

    public event Action ButtonClicked;

    private void OnEnable() => 
        _button.onClick.AddListener(OnButtonPressed);

    private void OnDisable() => 
        _button.onClick.RemoveListener(OnButtonPressed);

    public void Initialize(Sprite icon, int price)
    {
        _icon.sprite = icon;
        _price.text = price.ToString();
    }

    private void OnButtonPressed() => 
        ButtonClicked?.Invoke();
}
