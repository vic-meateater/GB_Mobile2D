using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Feature.AbilitySystem.Abilities;

namespace Feature.AbilitySystem
{
    internal interface IAbilitiesController
    { }

    internal class AbilitiesController : BaseController, IAbilitiesController
    {
        private readonly IAbilitiesView _view;
        private readonly IAbilitiesRepository _repository;
        private readonly IAbilityActivator _abilityActivator;

        public AbilitiesController(
            [NotNull] IAbilitiesView view,
            [NotNull] IAbilitiesRepository repository,
            [NotNull] IEnumerable<IAbilityItem> abilityItemConfigs,
            [NotNull] IAbilityActivator abilityActivator)
        {
            _abilityActivator = abilityActivator ?? throw new ArgumentNullException(nameof(abilityActivator));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            
            if(abilityItemConfigs == null)
                throw new ArgumentNullException(nameof(abilityItemConfigs));
            
            _view.Display(abilityItemConfigs, OnAbilityViewClicked);
        }

        private void OnAbilityViewClicked(string abilityId)
        {
            if (_repository.Items.TryGetValue(abilityId, out IAbility ability))
                ability.Apply(_abilityActivator);
        }
    }
}