using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Models
{
    public class CatalogItemRepository: ICatalogItemRepository
    {
        private static ConcurrentDictionary<int, CatalogItem> items =
          new ConcurrentDictionary<int, CatalogItem>();

        public CatalogItemRepository()
        {
            //Add(new Item { Id = Guid.NewGuid().ToString(), Text = "Item 1", Description = "This is an item description." });
            //Add(new Item { Id = Guid.NewGuid().ToString(), Text = "Item 2", Description = "This is an item description." });
            //Add(new Item { Id = Guid.NewGuid().ToString(), Text = "Item 3", Description = "This is an item description." });
        }

        public CatalogItem Get(int id)
        {
            return items[id];
        }

        public IEnumerable<CatalogItem> GetAll()
        {
            return items.Values;
        }

        public void Add(CatalogItem item)
        {
            //item.Id = Guid.NewGuid().ToString();
            items[item.Id] = item;
        }

        public CatalogItem Find(int id)
        {
            CatalogItem item;
            items.TryGetValue(id, out item);

            return item;
        }

        public CatalogItem Remove(int id)
        {
            CatalogItem item;
            items.TryRemove(id, out item);

            return item;
        }

        public void Update(CatalogItem item)
        {
            items[item.Id] = item;
        }
    }
}

