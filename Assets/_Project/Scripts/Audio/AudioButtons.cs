using UnityEngine;
using UnityEngine.UI;

public class AudioButtons : MonoBehaviour
{
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _musicButton;

    [SerializeField] private Sprite _soundOn;
    [SerializeField] private Sprite _soundOff;

    [SerializeField] private Sprite _musicOn;
    [SerializeField] private Sprite _musicOff;

    [SerializeField] private Image _soundImage;
    [SerializeField] private Image _musicImage;

    private IAudioHandler _audioHandler;

    private void OnDestroy()
    {
        _soundButton.onClick.RemoveListener(SwitchMuteSound);
        _musicButton.onClick.RemoveListener(SwitchMuteMusic);
    }

    public void Construct(IAudioHandler audioHandler)
    {
        _audioHandler = audioHandler;

        _soundButton.onClick.AddListener(SwitchMuteSound);
        _musicButton.onClick.AddListener(SwitchMuteMusic);

        ResetSettings();
    }

    private void SwitchMuteSound()
    {
        _audioHandler.SwitchSoundMute(!_audioHandler.IsSoundMute);
        SetSoundSprite();
    }

    private void SwitchMuteMusic()
    {
        _audioHandler.SwitchMusicMute(!_audioHandler.IsMusicMute);
        SetMusicSprite();
    }

    private void SetSoundSprite() =>
        _soundImage.sprite =
        _audioHandler.IsSoundMute
        ? _soundOff
        : _soundOn;

    private void SetMusicSprite() =>
        _musicImage.sprite =
        _audioHandler.IsMusicMute
        ? _musicOff
        : _musicOn;

    private void ResetSettings()
    {        
        SetSoundSprite();
        SetMusicSprite();
    }
}
