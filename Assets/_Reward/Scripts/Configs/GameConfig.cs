using UnityEngine;

[CreateAssetMenu(fileName = nameof(GameConfig), menuName = "Configs/Rewards/" + nameof(GameConfig))]
internal class GameConfig : ScriptableObject
{
    [field: Header("Game Settings")]
    [field: SerializeField] public RewardsConfig RewardsConfig { get; private set; }
}