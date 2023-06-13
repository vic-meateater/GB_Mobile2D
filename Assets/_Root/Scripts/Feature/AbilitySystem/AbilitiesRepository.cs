using System.Collections.Generic;
using Feature.AbilitySystem.Abilities;

namespace Feature.AbilitySystem
{
    internal interface IAbilitiesRepository : IRepository
    {
        IReadOnlyDictionary<string, IAbility> Items { get; }
    }

    internal class AbilitiesRepository : BaseRepository<string, IAbility, IAbilityItem>, IAbilitiesRepository
    {
        public AbilitiesRepository(IEnumerable<IAbilityItem> abilityItems) : base(abilityItems)
        { }

        protected override string GetKey(IAbilityItem abilityItem) => abilityItem.Id;

        protected override IAbility CreateItem(IAbilityItem config) =>
            config.Type switch
            {
                AbilityType.Gun => new GunAbility(config),
                AbilityType.Rocket => new RocketLaunchAbility(config),
                _ => DefaultAbility.Default
            };
    }
}
