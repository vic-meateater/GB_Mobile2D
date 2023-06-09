using System.Collections.Generic;
using UnityEngine;

namespace Feature.Inventory.Items
{
    [CreateAssetMenu(fileName = nameof(ItemConfigDataSource), menuName = "Configs/Inventory/" + nameof(ItemConfigDataSource))]
    internal class ItemConfigDataSource : ScriptableObject
    {
        [SerializeField] private ItemConfig[] _itemConfigs;

        public IReadOnlyList<ItemConfig> ItemConfigs => _itemConfigs;
    }
}