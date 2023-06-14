using Feature.AbilitySystem;
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
        private readonly TapeBackgroundController _tapeBackgroundController;
        private readonly AbilitiesContext _abilitiesContext;
        
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
            
            _abilitiesContext = CreateAbilitiesContext(placeForUi, carController);
        }

        private AbilitiesContext CreateAbilitiesContext(Transform placeForUi, IAbilityActivator abilityActivator)
        {
            AbilitiesContext сontext = new(placeForUi, abilityActivator);
            AddDisposable(сontext);

            return сontext;
        }
    }
}