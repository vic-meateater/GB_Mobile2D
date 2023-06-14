using Feature.Inventory.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.Inventory
{
    internal interface IInventoryView
    {
        void Dispalay(IEnumerable<IItem> itemsCollections, Action<string> itemClicked);
        void Clear();
        void Select(string id);
        void Unselect(string id);
    }

    internal class InventoryView : MonoBehaviour, IInventoryView
    {
        [SerializeField] private GameObject _itemViewPrefab;
        [SerializeField] private Transform _placeForItems;

        private readonly Dictionary<string, ItemView> _itemViews = new();

        public void Dispalay(IEnumerable<IItem> itemsCollections, Action<string> itemClicked)
        {
            Clear();

            foreach (IItem item in itemsCollections)
            {
                _itemViews[item.Id] = CreateItemView(item, itemClicked);
            }
        }


        public void Clear()
        {
            foreach(ItemView itemView in _itemViews.Values)
            {
                DestroyItemView(itemView);
            }
            _itemViews.Clear();
        }


        public void Select(string id) => _itemViews[id].Select();

        public void Unselect(string id) => _itemViews[id].Unselect();

        private ItemView CreateItemView(IItem item, Action<string> itemClicked)
        {
            GameObject objectView = Instantiate(_itemViewPrefab, _placeForItems);
            ItemView itemView = objectView.GetComponent<ItemView>();

            itemView.Init(item, () => itemClicked?.Invoke(item.Id));
            return itemView;
        }

        private void DestroyItemView(ItemView itemView)
        {
            itemView.DeInit();
            Destroy(itemView.gameObject);
        }
    }
}
