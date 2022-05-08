using UnityEngine;

[CreateAssetMenu(fileName = nameof(SoundSettings), menuName =("Settings/SoundSettings"))]
public class SoundSettings : ScriptableObject
{
    [SerializeField] [Range(0, 100)] private float _music;
    [SerializeField] [Range(0, 100)] private float _sfx;
}
