using System;

public interface IAudioHandler : IService
{
    bool IsMusicMute { get; }
    bool IsSoundMute { get; }
    float MusicValue { get; }
    float SoundValue { get; }

    event Action AudioChanged;

    void ChangeMusicVolume(float value);
    void ChangeSoundVolume(float value);
    void SwitchMusicMute(bool isMute);
    void SwitchSoundMute(bool isMute);
}