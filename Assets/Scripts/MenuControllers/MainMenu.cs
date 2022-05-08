using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;

    private Transform _canvasTransform;

    private Animator _animator;

    private Action _onClose;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _canvasTransform = FindObjectOfType<Canvas>().transform;

        _startButton.onClick.AddListener(OnStartGame);
        _settingsButton.onClick.AddListener(OnSettingsOpen);
    }

    private void OnStartGame()
    {
        _onClose += () => SceneManager.LoadScene(1);
        _animator.SetTrigger("Close");
    }

    private void OnSettingsOpen()
    {
        var settingsPrefab = Resources.Load("Menus/SettingsPanel");
        Instantiate(settingsPrefab, _canvasTransform);
    }

    private void OnAnimationClose()
    {
        _onClose?.Invoke();
    }
}
