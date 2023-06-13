using System.Collections.Generic;
using Feature.AbilitySystem;
using Feature.AbilitySystem.Abilities;
using Game.Car;
using Game.InputLogic;
using Game.TapeBackground;
using Profile;
using Tool;
using UnityEngine;

namespace Game
{
    internal class GameController : BaseController
    {
        private readonly SubscriptionProperty<float> _leftMoveDiff;
        private readonly SubscriptionProperty<float> _rightMoveDiff;

        private readonly CarController _carController;
        private readonly InputGameController _inputGameController;
        private readonly IAbilitiesController _abilitiesController;
        private readonly TapeBackgroundController _tapeBackgroundController;
        
        public GameController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _leftMoveDiff = new SubscriptionProperty<float>();
            _rightMoveDiff = new SubscriptionProperty<float>();

            _tapeBackgroundController = new TapeBackgroundController(_leftMoveDiff, _rightMoveDiff);
            AddDisposable(_tapeBackgroundController);

            var inputGameController = new InputGameController(_leftMoveDiff, _rightMoveDiff, profilePlayer.CurrentCar);
            AddDisposable(inputGameController);

            var carController = new CarController();
            AddDisposable(carController);
            
            _abilitiesController = CreateAbilitiesController(placeForUi, carController);
        }

        private IAbilitiesController CreateAbilitiesController(Transform placeForUi, IAbilityActivator abilityActivator)
        {
            AbilityItemConfig[] itemConfigs = LoadAbilityItemConfigs();
            AbilitiesRepository repository = CreateAbilitiesRepository(itemConfigs);
            AbilitiesView view = LoadAbilitiesView(placeForUi);
            
            AbilitiesController controller = new(view, repository, itemConfigs, abilityActivator);
            AddDisposable(controller);

            return controller;
        }
        
        private AbilityItemConfig[] LoadAbilityItemConfigs()
        {
            ResourcePath _dataSourcePath = new("Configs/Ability/AbilityItemConfigDataSource");
            return ContentDataSourceLoader.LoadAbilityItemConfigs(_dataSourcePath);
        }

        private AbilitiesRepository CreateAbilitiesRepository(IEnumerable<IAbilityItem> abilityItemConfigs)
        {
            AbilitiesRepository repository = new(abilityItemConfigs);
            AddDisposable(repository);

            return repository;
        }

        private AbilitiesView LoadAbilitiesView(Transform placeForUi)
        {
            ResourcePath _viewPath = new("Prefabs/Ability/AbilitiesView");
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<AbilitiesView>();
        }
    }
}
