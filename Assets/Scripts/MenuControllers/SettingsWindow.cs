using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class SettingsWindow : MonoBehaviour
{
    [SerializeField] private Slider _music;
    [SerializeField] private Slider _sfx;

    [SerializeField] private Button _close;

    private Animator _animator;
    [Zenject.Inject]
    private SoundSettings _soundSettings;

    private Action _onClose;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _close.onClick.AddListener(CloseWindow);

        _music.value = _soundSettings.MusicVolume;
        _sfx.value = _soundSettings.SfxVolume;

        _music.onValueChanged.AddListener((float value) => { _soundSettings.MusicVolume = value; });
        _sfx.onValueChanged.AddListener((float value) => { _soundSettings.SfxVolume = value; });
    }

    private void CloseWindow()
    {
        _onClose += () => Destroy(gameObject);
        _animator.SetTrigger("Close");
    }

    private void OnAnimationClose()
    {
        _onClose?.Invoke();
    }
}
