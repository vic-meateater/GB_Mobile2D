using System.Collections.Generic;

namespace Feature.Inventory
{
    internal interface IInventoryModel
    {
        IReadOnlyList<string> EquppedItems { get; }
        void EquipItem(string itemId);
        void UnequipItem(string itemId);
        bool IsEquipped(string itemId);

    }

    internal class InventoryModel: IInventoryModel
    {
        private readonly List<string> _equippedItems = new();
        public IReadOnlyList<string> EquppedItems => _equippedItems;

        public void EquipItem(string itemId)
        {
            if(!IsEquipped(itemId))
                _equippedItems.Add(itemId);
        }
        public void UnequipItem(string itemId)
        {
            if (IsEquipped(itemId))
                _equippedItems.Remove(itemId);
        }

        public bool IsEquipped(string itemId)
        {
            return _equippedItems.Contains(itemId);
        }

    }
}
