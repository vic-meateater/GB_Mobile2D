﻿using Feature.Inventory.Items;
using System;
using System.Linq;

namespace Tool
{
    internal static class ContentDataSourceLoader
    {
        public static ItemConfig[] LoadItemConfigs(ResourcePath resourcePath)
        {
            var dataSource = ResourcesLoader.LoadObject<ItemConfigDataSource>(resourcePath);
            return dataSource == null ? Array.Empty<ItemConfig>() : dataSource.ItemConfigs.ToArray();
        }
    }
}
