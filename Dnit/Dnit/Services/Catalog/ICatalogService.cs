using Dnit.Core.Models.Catalog;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Dnit.Core.Services.Catalog
{
    public interface ICatalogService
    {
        Task<ObservableCollection<CatalogItem>> GetCatalogAsync();
        Task<ObservableCollection<CatalogItem>> FilterAsync(string productfilter);
        Task<ObservableCollection<CatalogType>> GetCatalogTypeAsync();
        Task<ObservableCollection<CatalogBrand>> GetCatalogBrandAsync();

    }
}
