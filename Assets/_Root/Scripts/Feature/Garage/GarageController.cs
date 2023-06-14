using Feature.Garage.Upgrade;
using Feature.Inventory;
using JetBrains.Annotations;
using Profile;
using System;
using System.Collections.Generic;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Feature.Garage
{
    internal interface IGarageController
    {
    }

    internal class GarageController : BaseController, IGarageController
    {
        private readonly ResourcePath _viewPath = new("Prefabs/Garage/GarageView");
        private readonly ResourcePath _dataSourcePath = new("Configs/Garage/UpgradeItemConfigDataSource");

        private readonly GarageView _view;
        private readonly ProfilePlayer _profilePlayer;
        private readonly InventoryContext _inventoryContext;
        private readonly IUpgradeHandlersRepository _upgradeHandlersRepository;

        public GarageController(
            [NotNull] Transform placeForUi,
            [NotNull] ProfilePlayer profilePlayer)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            _profilePlayer = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));

            _upgradeHandlersRepository = CreateRepository();
            _inventoryContext = CreateInventoryContext(placeForUi, _profilePlayer.Inventory);
            _view = LoadView(placeForUi);

            _view.Init(Apply, Back);
        }

        private InventoryContext CreateInventoryContext(Transform placeForUi, IInventoryModel profilePlayerInventory)
        {
            InventoryContext context = new(placeForUi, profilePlayerInventory);
            AddDisposable(context);
            return context;
        }


        private GarageView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<GarageView>();
        }
        
        private UpgradeHandlersRepository CreateRepository()
        {
            UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(_dataSourcePath);
            UpgradeHandlersRepository repository = new(upgradeConfigs);
            AddDisposable(repository);
            return repository;
        }

        private void Apply()
        {
            _profilePlayer.CurrentCar.Restore();

            UpgradeWithEquippedItems(
                _profilePlayer.CurrentCar,
                _profilePlayer.Inventory.EquppedItems,
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
