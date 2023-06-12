using UnityEngine;
using System.Collections.Generic;

namespace Feature.AbilitySystem.Abilities
{
    [CreateAssetMenu(
        fileName = nameof(AbilityItemConfigDataSource),
        menuName = "Configs/Ability/" + nameof(AbilityItemConfigDataSource))]
    internal class AbilityItemConfigDataSource : ScriptableObject
    {
        [SerializeField] private AbilityItemConfig[] _abilityConfigs;

        public IReadOnlyList<AbilityItemConfig> AbilityConfigs => _abilityConfigs;
    }
}
