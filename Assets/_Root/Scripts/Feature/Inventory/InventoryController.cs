using Feature.Inventory.Items;
using JetBrains.Annotations;
using System;

namespace Feature.Inventory
{
    internal interface IInventoryController
    {
    }
    internal class InventoryController: BaseController, IInventoryController
    {
        private readonly IInventoryView _inventoryView;
        private readonly IInventoryModel _inventoryModel;
        private readonly IItemsRepository _itemsRepository;

        public InventoryController(
            [NotNull] IInventoryView inventoryView,
            [NotNull] IInventoryModel inventoryModel,
            [NotNull] IItemsRepository inventoryRepository)
        {

            _inventoryView = inventoryView ?? throw new ArgumentNullException(nameof(inventoryView));
            _inventoryModel = inventoryModel ?? throw new ArgumentNullException(nameof(inventoryModel));
            _itemsRepository = inventoryRepository ?? throw new ArgumentNullException(nameof(inventoryRepository));

            _inventoryView.Dispalay(_itemsRepository.Items.Values, OnDisplayClicked);

            foreach(string itemId in _inventoryModel.EquppedItems)
            {
                _inventoryView.Select(itemId);
            }
        }
        
        protected override void OnDispose() => _inventoryView.Clear();

        
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

        private void EquipItem(string itemId)
        {
            _inventoryView.Select(itemId);
            _inventoryModel.EquipItem(itemId);
        }
        private void UnequipItem(string itemId)
        {
            _inventoryView.Unselect(itemId);
            _inventoryModel.UnequipItem(itemId);
        }
    }
}