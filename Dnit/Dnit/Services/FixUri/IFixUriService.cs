using System.Collections.Generic;
using Dnit.Core.Models.Catalog;


namespace Dnit.Core.Services.FixUri
{
    public interface IFixUriService
    {
        void FixCatalogItemPictureUri(IEnumerable<CatalogItem> catalogItems);
       
    }
}
