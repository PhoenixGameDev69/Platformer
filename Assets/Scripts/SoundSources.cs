using UnityEngine;

public class SoundSources : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Zenject.Inject]
    private SoundSettings _soundSettings;

    private void Awake()
    {
        _musicSource.volume = _soundSettings.MusicVolume;
        _sfxSource.volume = _soundSettings.SfxVolume;

        _soundSettings.OnMusicVolumeChanged += ChangeMusicVolume;
        _soundSettings.OnSfxVolumeChanged += ChangeSfxVolume;
    }

    private void ChangeMusicVolume(float value)
    {
        _musicSource.volume = value;
    }

    private void ChangeSfxVolume(float value)
    {
        _sfxSource.volume = value;
    }

    private void OnDestroy()
    {
        _soundSettings.OnMusicVolumeChanged -= ChangeMusicVolume;
        _soundSettings.OnSfxVolumeChanged -= ChangeSfxVolume;
    }
}