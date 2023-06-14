using System.Collections.Generic;

namespace Feature.Inventory.Items
{
    internal interface IItemsRepository
    {
        IReadOnlyDictionary<string, IItem> Items { get; }
    }

    internal class ItemsRepository : BaseRepository<string, IItem, ItemConfig>, IItemsRepository
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
