using Feature.AbilitySystem;
using Feature.Fight;
using Game.Car;
using Game.InGameMenu;
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
        private readonly TapeBackgroundController _tapeBackgroundController;
        private readonly StartFightController _startFightController;
        private readonly StartInGameMenuController _inGameMenuController;
        private readonly AbilitiesContext _abilitiesContext;
        
        public GameController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _leftMoveDiff = new SubscriptionProperty<float>();
            _rightMoveDiff = new SubscriptionProperty<float>();

            _tapeBackgroundController = new TapeBackgroundController(_leftMoveDiff, _rightMoveDiff);
            AddDisposable(_tapeBackgroundController);

            _inputGameController = new InputGameController(_leftMoveDiff, _rightMoveDiff, profilePlayer.CurrentCar);
            AddDisposable(_inputGameController);

            _carController = new CarController();
            AddDisposable(_carController);

            _startFightController = new StartFightController(placeForUi, profilePlayer);
            AddDisposable(_startFightController);

            _inGameMenuController = new StartInGameMenuController(placeForUi, profilePlayer);
            AddDisposable(_inGameMenuController);
            
            _abilitiesContext = CreateAbilitiesContext(placeForUi, _carController);
        }

        private AbilitiesContext CreateAbilitiesContext(Transform placeForUi, IAbilityActivator abilityActivator)
        {
            AbilitiesContext сontext = new(placeForUi, abilityActivator);
            AddDisposable(сontext);

            return сontext;
        }
    }
}