using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private CanvasGroup _group;
    [SerializeField] private AudioPlayer _audioPlayer;
    [SerializeField] private AudioClip _attackSound;

    public void SetAlpha(float value) =>
        _group.alpha = value;

    public void PlayAttackSound() =>
        _audioPlayer.PlaySound(_attackSound);
}