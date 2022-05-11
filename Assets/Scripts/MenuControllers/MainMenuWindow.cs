using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class MainMenuWindow : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;

    private Transform _canvasTransform;
    private Animator _animator;

    private Action _onClose;

    [Zenject.Inject]
    private SoundSettings _soundSettings;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _canvasTransform = FindObjectOfType<Canvas>().transform;

        _startButton.onClick.AddListener(OnStartGame);
        _settingsButton.onClick.AddListener(OnSettingsOpen);
        _exitButton.onClick.AddListener(OnExitGame);

        Debug.Log(_soundSettings.MusicVolume);
    }

    private void OnStartGame()
    {
        _onClose += () => SceneManager.LoadScene(1);
        _animator.SetTrigger("Close");
    }

    private void OnSettingsOpen()
    {
        var settingsPrefab = Resources.Load("Menus/SettingsWindow");
        Instantiate(settingsPrefab, _canvasTransform);
    }

    private void OnExitGame()
    {
        _onClose += () =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        };

        _animator.SetTrigger("Close");
    }

    private void OnAnimationClose()
    {
        _onClose?.Invoke();
    }
}
