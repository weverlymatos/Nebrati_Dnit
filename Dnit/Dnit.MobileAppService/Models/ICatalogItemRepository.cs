using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Models
{
    public interface ICatalogItemRepository
    {
        void Add(CatalogItem item);
        void Update(CatalogItem item);
        CatalogItem Remove(int key);
        CatalogItem Get(int id);
        IEnumerable<CatalogItem> GetAll();
    }
}
