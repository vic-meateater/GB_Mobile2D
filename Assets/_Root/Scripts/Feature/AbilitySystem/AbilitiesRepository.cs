using System.Collections.Generic;
using Feature.AbilitySystem.Abilities;

namespace Feature.AbilitySystem
{
    internal interface IAbilitiesRepository : IRepository
    {
        IReadOnlyDictionary<string, IAbility> Items { get; }
    }

    internal class AbilitiesRepository : BaseRepository<string, IAbility, AbilityItemConfig>
    {
        public AbilitiesRepository(IEnumerable<AbilityItemConfig> configs) : base(configs)
        { }

        protected override string GetKey(AbilityItemConfig config) => config.Id;

        protected override IAbility CreateItem(AbilityItemConfig config) =>
            config.Type switch
            {
                AbilityType.Gun => new GunAbility(config),
                AbilityType.Rocket => new RocketLaunchAbility(config),
                _ => DefaultAbility.Default
            };
    }
}
