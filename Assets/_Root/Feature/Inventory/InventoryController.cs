using Feature.Inventory.Items;
using JetBrains.Annotations;
using System;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Feature.Inventory
{
    internal class InventoryController: BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("");
        private readonly ResourcePath _dataSourcePath = new ResourcePath("");

        private readonly InventoryView _inventoryView;
        private readonly IInventoryModel _inventoryModel;
        private readonly ItemsRepository _itemsRepository;

        public InventoryController(
            [NotNull] Transform placeFirUi, 
            [NotNull] IInventoryModel inventoryModel)
        {
            if(placeFirUi == null)
                throw new ArgumentNullException(nameof(placeFirUi));

            _inventoryModel = inventoryModel ?? throw new ArgumentNullException(nameof(inventoryModel));

            _itemsRepository = CreateRepository();
            _inventoryView = LoadView(placeFirUi);

            _inventoryView.Dispalay(_itemsRepository.Items.Values, OnDisplayClicked);

            foreach(string itemId in _inventoryModel.EquppedItems)
            {
                _inventoryView.Select(itemId);
            }

        }


        private ItemsRepository CreateRepository()
        {
            ItemConfig[] itemConfigs = ContentDataSourceLoader.LoadItemConfigs(_dataSourcePath);
            ItemsRepository repository = new(itemConfigs);
            AddRepository(repository);
            return repository;
        }

        private InventoryView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi);
            AddGameObject(objectView);

            return objectView.GetComponent<InventoryView>();
        }
        private void OnDisplayClicked(string ItemId)
        {
            bool isEquipped = _inventoryModel.IsEquipped(ItemId);

            if(isEquipped)
            {
                UnequipItem(ItemId);
            }
            else
            {
                EquipItem(ItemId);
            }

        }

        private void UnequipItem(string itemId)
        {
            _inventoryView.Unselect(itemId);
            _inventoryModel.EquipItem(itemId);
        }
        private void EquipItem(string itemId)
        {
            _inventoryView.Select(itemId);
            _inventoryModel.UnequipItem(itemId);
        }
    }
}