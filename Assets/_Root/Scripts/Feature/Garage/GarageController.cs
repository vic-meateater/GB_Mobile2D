using Feature.Garage.Upgrade;
using Feature.Inventory;
using JetBrains.Annotations;
using Profile;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.Garage
{
    internal interface IGarageController
    { }

    internal class GarageController : BaseController, IGarageController
    {
        private readonly IGarageView _view;
        private readonly ProfilePlayer _profilePlayer;
        private readonly IUpgradable _currentCar;
        private readonly IUpgradeHandlersRepository _upgradeHandlersRepository;
        private readonly IInventoryModel _inventoryModel;

        public GarageController(
            [NotNull] ProfilePlayer profilePlayer,
            [NotNull] IUpgradable currentCar,
            [NotNull] IUpgradeHandlersRepository upgradeHandlersRepository,
            [NotNull] IInventoryModel inventoryModel,
            [NotNull] IGarageView view)
        {
            _profilePlayer = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));
            _upgradeHandlersRepository = 
                upgradeHandlersRepository ?? throw new ArgumentNullException(nameof(upgradeHandlersRepository));
            
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _inventoryModel = inventoryModel ?? throw new ArgumentNullException(nameof(inventoryModel));
            _currentCar = currentCar ?? throw new ArgumentNullException(nameof(currentCar));

            _view.Init(Apply, Back);
        }

        private void Apply()
        {
            _profilePlayer.CurrentCar.Restore();

            UpgradeWithEquippedItems(
                _currentCar,
                _inventoryModel.EquppedItems,
                _upgradeHandlersRepository.Items);

            _profilePlayer.CurrentState.Value = GameState.Start;
            Log($"Apply. Speed: {_profilePlayer.CurrentCar.Speed}");
            Log($"Apply. JumpHeight: {_profilePlayer.CurrentCar.JumpHeight}");
        }

        private void Back()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
            Log($"Back. Speed: {_profilePlayer.CurrentCar.Speed}");
            Log($"Back. JumpHeight: {_profilePlayer.CurrentCar.JumpHeight}");
        }

        private void UpgradeWithEquippedItems(IUpgradable upgradable, 
                                              IReadOnlyList<string> equppedItems, 
                                              IReadOnlyDictionary<string, IUpgradeHandler> upgradeHandlers)
        {
            foreach(string itemId in equppedItems)
            {
                if (upgradeHandlers.TryGetValue(itemId, out IUpgradeHandler upgradeHandler))
                    upgradeHandler.Upgrade(upgradable);
            }
        }

        private void Log(string message) =>
            Debug.Log($"[{GetType().Name}] {message}");
    }
}
