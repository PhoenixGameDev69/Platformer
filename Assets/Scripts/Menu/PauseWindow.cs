using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseWindow : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _returnToMainMenuButton;

    private Transform _canvasTransform;
    private Animator _animator;
    private Zenject.DiContainer _diContainer;

    private Action _onClose;

    [Zenject.Inject]
    public void Construct(Zenject.DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _canvasTransform = FindObjectOfType<Canvas>().transform;
    }

    private void OnEnable()
    {
        Time.timeScale = 0.0f;

        _continueButton.onClick.AddListener(OnContinue);
        _settingsButton.onClick.AddListener(OnSettingsOpen);
        _restartButton.onClick.AddListener(OnRestartScene);
        _returnToMainMenuButton.onClick.AddListener(OnReturnToMainMenu);
    }

    private void OnContinue()
    {
        _onClose += () => { Destroy(gameObject); };
        _animator.SetTrigger("Close");
    }

    private void OnSettingsOpen()
    {
        var settingsPrefab = Resources.Load(GlobalStringVars.SettingsWindowPath);
        _diContainer.InstantiatePrefab(settingsPrefab, _canvasTransform);
    }

    private void OnReturnToMainMenu()
    {
        _onClose += () => { SceneManager.LoadScene(0); };

        _animator.SetTrigger("Close");
    }

    private void OnRestartScene()
    {
        _onClose += () =>
        {
            var currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        };

        _animator.SetTrigger("Close");
    }

    private void OnAnimationClose()
    {
        _onClose?.Invoke();
    }

    private void OnDestroy()
    {
        _continueButton.onClick.RemoveListener(OnContinue);
        _settingsButton.onClick.RemoveListener(OnSettingsOpen);
        _restartButton.onClick.RemoveListener(OnRestartScene);
        _returnToMainMenuButton.onClick.RemoveListener(OnReturnToMainMenu);

        Time.timeScale = 1.0f;
    }
}
