using System.Collections.Generic;

namespace Feature.Inventory.Items
{
    internal interface IItemsRepository
    {
        IReadOnlyDictionary<string, IItem> GetItems();
    }

    internal class ItemsRepository : BaseRepository<string, IItem, ItemConfig>
    {
        public ItemsRepository(IEnumerable<ItemConfig> configs) : base(configs)
        {
        }

        protected override IItem CreateItem(ItemConfig config) =>
            new Item(
                config.Id,
                new ItemInfo(
                    config.Title, 
                    config.Icon
                )
            );

        protected override string GetKey(ItemConfig config) => config.Id;
    }
}
