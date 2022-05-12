using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;

    [Zenject.Inject]
    private readonly Zenject.DiContainer _diContainer;

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(OnPause);
    }

    private void OnPause()
    {
        var pauseWindow = Resources.Load(GlobalStringVars.PauseWindowPath);
        _diContainer.InstantiatePrefab(pauseWindow, transform);
    }

    private void OnDestroy()
    {
        _pauseButton.onClick.RemoveListener(OnPause);
    }
}
