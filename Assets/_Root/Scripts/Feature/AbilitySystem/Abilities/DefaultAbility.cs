namespace Feature.AbilitySystem.Abilities
{
    internal class DefaultAbility : IAbility
    {
        public static readonly IAbility Default = new DefaultAbility();

        public void Apply(IAbilityActivator activator)
        { }
    }
}
