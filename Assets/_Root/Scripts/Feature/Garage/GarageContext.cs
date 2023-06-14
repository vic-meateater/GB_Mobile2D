using System;
using Feature.Garage.Upgrade;
using Feature.Inventory;
using JetBrains.Annotations;
using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Feature.Garage
{
    internal class GarageContext: BaseContext
    {
        private readonly ResourcePath _viewPath = new("Prefabs/Garage/GarageView");
        private readonly ResourcePath _dataSourcePath = new("Configs/Garage/UpgradeItemConfigDataSource");

        public GarageContext([NotNull] Transform placeForUi, [NotNull] ProfilePlayer profilePlayer)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            if (profilePlayer == null)
                throw new ArgumentNullException(nameof(profilePlayer));

            CreateController(placeForUi, profilePlayer);
        }

        private void CreateController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            IUpgradeHandlersRepository repository = CreateRepository();
            IInventoryContext context = CreateInventoryContext(placeForUi, profilePlayer.Inventory);
            GarageView view = LoadView(placeForUi);
            GarageController controller = 
                new(profilePlayer, profilePlayer.CurrentCar, 
                    repository, profilePlayer.Inventory, view);
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
        
        private InventoryContext CreateInventoryContext(Transform placeForUi, IInventoryModel profilePlayerInventory)
        {
            InventoryContext context = new(placeForUi, profilePlayerInventory);
            AddDisposable(context);
            return context;
        }
    }
}