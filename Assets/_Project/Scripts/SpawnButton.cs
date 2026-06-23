using System;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour
{    
    [SerializeField] private Button _button;       

    public event Action ButtonClicked;

    private void OnEnable() => 
        _button.onClick.AddListener(OnButtonPressed);

    private void OnDisable() => 
        _button.onClick.RemoveListener(OnButtonPressed);

    private void OnButtonPressed() => 
        ButtonClicked?.Invoke();
}
