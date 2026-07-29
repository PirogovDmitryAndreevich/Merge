using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundsSource;

    [Header("Background music")]
    [SerializeField] private AudioClip _mainMusic;
    [SerializeField] private AudioClip _battleMusic;

    private IAudioHandler _audioHandler;

    private void OnDestroy()
    {
        _audioHandler.AudioChanged -= ResetVolume;
    }

    public void Construct(IAudioHandler audioHandler)
    {
        _audioHandler = audioHandler;

        _audioHandler.AudioChanged += ResetVolume;

        SettingSources();
        ResetVolume();
        PlayMainMusic();
    }

    public void PlayMainMusic() =>
        PlayMusic(_mainMusic);

    public void PlayBattleMusic() => 
        PlayMusic(_battleMusic);

    public void PlaySound(AudioClip clip) =>
        _soundsSource.PlayOneShot(clip);

    private void PlayMusic(AudioClip clip)
    {
        _musicSource.Stop();
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    private void ResetVolume()
    {
        _soundsSource.mute = _audioHandler.IsSoundMute;
        _musicSource.mute = _audioHandler.IsMusicMute;

        _soundsSource.volume = _audioHandler.SoundValue;
        _musicSource.volume = _audioHandler.MusicValue;
    }

    private void SettingSources()
    {
        _musicSource.playOnAwake = true;
        _musicSource.loop = true;

        _soundsSource.loop = false;
        _soundsSource.playOnAwake = false;
    }
}