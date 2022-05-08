using System;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider _music;
    [SerializeField] private Slider _sfx;

    [SerializeField] private Button _close;

    private Animator _animator;

    private Action _onClose;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _close.onClick.AddListener(CloseWindow);
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
