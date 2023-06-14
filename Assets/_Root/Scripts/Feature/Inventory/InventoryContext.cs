using System;
using Feature.Inventory.Items;
using JetBrains.Annotations;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Feature.Inventory
{
    internal class InventoryContext : BaseContext
    {
        private static readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Inventory/InventoryView");
        private static readonly ResourcePath _dataSourcePath = new ResourcePath("Configs/Inventory/ItemConfigDataSource");
        
        public InventoryContext([NotNull] Transform placeForUi, [NotNull] IInventoryModel inventory)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            if (inventory == null)
                throw new ArgumentNullException(nameof(inventory));
            
            CreateInventoryController(placeForUi, inventory);
        }
        
        private InventoryController CreateInventoryController(Transform placeForUi, IInventoryModel inventoryModel)
        {
            InventoryView inventoryView = LoadInventoryView(placeForUi);
            IItemsRepository itemRepository = CreateItemRepository();
            InventoryController inventoryController = new(inventoryView, inventoryModel, itemRepository);
            AddDisposable(inventoryController);
            
            return inventoryController;
        }
        private InventoryView LoadInventoryView(Transform placeForUi)
        {
            
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi);
            AddGameObject(objectView);

            return objectView.GetComponent<InventoryView>();
        }
        
        private ItemConfig[] LoadConfigs() => ContentDataSourceLoader.LoadItemConfigs(_dataSourcePath);
        
        private ItemsRepository CreateItemRepository()
        {
            ItemConfig[] itemConfigs = LoadConfigs();
            ItemsRepository repository = new(itemConfigs);
            AddDisposable(repository);
            return repository;
        }

        

    }
}
