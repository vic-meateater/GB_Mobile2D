using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(RewardsConfig), menuName = "Configs/Rewards/" + nameof(RewardsConfig))]
internal class RewardsConfig : ScriptableObject
{
    [field: SerializeField] public List<RewardConfig> Rewards { get; private set; }
}
