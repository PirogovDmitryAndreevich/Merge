using System;

[Serializable]
public class AudioSaveData
{
    public float SoundValue;
    public float MusicValue;
    public bool IsSoundMute;
    public bool IsMusicMute;

    public AudioSaveData()
    {
        SoundValue = 0.5f;
        MusicValue = 0.5f;
        IsSoundMute = false;
        IsMusicMute = false;
    }
}