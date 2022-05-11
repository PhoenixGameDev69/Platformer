using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    [SerializeField] private Player.Settings _playerSettings;
    [SerializeField] private SoundSettings _soundSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(_playerSettings);
        Container.BindInstance(_soundSettings);
    }
}