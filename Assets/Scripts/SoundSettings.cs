using System;
using UnityEngine;

[Serializable]
public class SoundSettings
{
    [SerializeField] [Range(0.0f, 1.0f)] private float _musicVolume;
    [SerializeField] [Range(0.0f, 1.0f)] private float _sfxVolume;

    public event Action<float> OnMusicVolumeChanged;
    public event Action<float> OnSfxVolumeChanged;

    public float MusicVolume
    {
        get => _musicVolume;
        set
        {
            if (value >= 0.0f && value <= 1.0f) { _musicVolume = value; }
            else if (value > 1.0f) { _musicVolume = 1.0f; }
            else { _musicVolume = 0.0f; }

            OnMusicVolumeChanged?.Invoke(_musicVolume);
        }
    }

    public float SfxVolume
    {
        get => _sfxVolume;
        set
        {
            if (value >= 0.0f && value <= 1.0f) { _sfxVolume = value; }
            else if (value > 1.0f) { _sfxVolume = 1.0f; }
            else { _sfxVolume = 0.0f; }

            OnSfxVolumeChanged?.Invoke(_sfxVolume);
        }
    }
}
