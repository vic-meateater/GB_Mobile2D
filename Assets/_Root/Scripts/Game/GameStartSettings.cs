using UnityEngine;

[CreateAssetMenu(fileName = nameof(GameStartSettings), menuName = "Configs/Game/" + nameof(GameStartSettings))]
internal class GameStartSettings : ScriptableObject
{
    [SerializeField] private float _speedCar = 15f;
    [SerializeField] private float _jumpHeight = 3f;
    
    public float SpeedCar => _speedCar;
    public float JumpHeight => _jumpHeight;
}
