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
    [Zenject.Inject]
    private readonly Zenject.DiContainer _diContainer;
    private Transform _canvasTransform;
    private Animator _animator;

    private Action _onClose;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _canvasTransform = FindObjectOfType<Canvas>().transform;

        _startButton.onClick.AddListener(OnStartGame);
        _settingsButton.onClick.AddListener(OnSettingsOpen);
        _exitButton.onClick.AddListener(OnExitGame);
    }

    private void OnStartGame()
    {
        _onClose += () => { SceneManager.LoadScene(1); };
        _animator.SetTrigger("Close");
    }

    private void OnSettingsOpen()
    {
        var settingsPrefab = Resources.Load(GlobalStringVars.SettingsWindowPath);
        _diContainer.InstantiatePrefab(settingsPrefab, _canvasTransform);
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

    private void OnDestroy()
    {
        _startButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
    }
}
