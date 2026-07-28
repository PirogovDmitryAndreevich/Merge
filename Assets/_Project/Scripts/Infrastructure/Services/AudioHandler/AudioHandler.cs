using System;

public class AudioHandler : IAudioHandler
{
    private readonly IProgressService _progressService;
    private readonly ISaveLoadService _save;

    public float MusicValue =>
        _progressService.Progress.Audio.MusicValue;
    public float SoundValue =>
        _progressService.Progress.Audio.SoundValue;
    public bool IsMusicMute =>
        _progressService.Progress.Audio.IsMusicMute;
    public bool IsSoundMute =>
        _progressService.Progress.Audio.IsSoundMute;

    public event Action AudioChanged;

    public AudioHandler(IProgressService progressService, ISaveLoadService save)
    {
        _progressService = progressService;
        _save = save;
    }

    public void SwitchMusicMute(bool isMute)
    {
        _progressService.Progress.Audio.IsMusicMute = isMute;
        _save.SaveProgress();
        AudioChanged?.Invoke();
    }

    public void SwitchSoundMute(bool isMute)
    {
        _progressService.Progress.Audio.IsSoundMute = isMute;
        _save.SaveProgress();
        AudioChanged?.Invoke();
    }

    public void ChangeMusicVolume(float value)
    {
        _progressService.Progress.Audio.MusicValue = value;
        _save.SaveProgress();
        AudioChanged?.Invoke();
    }

    public void ChangeSoundVolume(float value)
    {
        _progressService.Progress.Audio.SoundValue = value;
        _save.SaveProgress();
        AudioChanged?.Invoke();
    }
}
