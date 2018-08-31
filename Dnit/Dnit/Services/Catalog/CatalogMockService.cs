using Dnit.Core.Extensions;
using Dnit.Core.Models.Catalog;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Dnit.Core.Services.Catalog
{
    public class CatalogMockService : ICatalogService
    {
        private ObservableCollection<CatalogBrand> MockCatalogBrand = new ObservableCollection<CatalogBrand>
        {
            new CatalogBrand { Id = 1, Brand = "HP" },
            new CatalogBrand { Id = 2, Brand = "Accer" }
        };

        private ObservableCollection<CatalogType> MockCatalogType = new ObservableCollection<CatalogType>
        {
            new CatalogType { Id = 1, Type = "Computador" },
            new CatalogType { Id = 2, Type = "Acessórios" }
        };

        private ObservableCollection<CatalogItem> MockCatalog = new ObservableCollection<CatalogItem>
        {
            new CatalogItem { Id = Common.Common.MockCatalogItemId01, PictureUri = Device.RuntimePlatform != Device.UWP ? "fake_product_01.png" : "Assets/fake_product_01.png", Name = "Produto1", Price = 19.50M, CatalogBrandId = 2, CatalogBrand = "HP", CatalogTypeId = 2, CatalogType = "Computador" },
            new CatalogItem { Id = Common.Common.MockCatalogItemId02, PictureUri = Device.RuntimePlatform != Device.UWP ? "fake_product_02.png" : "Assets/fake_product_02.png", Name = "Produto2", Price = 19.50M, CatalogBrandId = 2, CatalogBrand = "HP", CatalogTypeId = 2, CatalogType = "Computador" },
            new CatalogItem { Id = Common.Common.MockCatalogItemId03, PictureUri = Device.RuntimePlatform != Device.UWP ? "fake_product_03.png" : "Assets/fake_product_03.png", Name = "Produto3)", Price = 19.95M, CatalogBrandId = 2, CatalogBrand = "HP", CatalogTypeId = 2, CatalogType = "Computador" },
            new CatalogItem { Id = Common.Common.MockCatalogItemId04, PictureUri = Device.RuntimePlatform != Device.UWP ? "fake_product_04.png" : "Assets/fake_product_04.png", Name = "Produto4", Price = 17.00M, CatalogBrandId = 2, CatalogBrand = "HP", CatalogTypeId = 1, CatalogType = "Acessórios" },
            new CatalogItem { Id = Common.Common.MockCatalogItemId05, PictureUri = Device.RuntimePlatform != Device.UWP ? "fake_product_05.png" : "Assets/fake_product_05.png", Name = "Produto5", Price = 19.50M, CatalogBrandId = 1, CatalogBrand = "Acer", CatalogTypeId = 2, CatalogType = "Acessórios" }
        };

        public async Task<ObservableCollection<CatalogItem>> GetCatalogAsync()
        {
            await Task.Delay(10);

            return MockCatalog.OrderBy(x=>x.Price).ToObservableCollection();
        }

        public async Task<ObservableCollection<CatalogItem>> FilterAsync(string productName)
        {
            await Task.Delay(10);
            return MockCatalog
                .Where(c => c.Name.ToUpper().Contains(productName.ToUpper())).OrderBy(x=>x.Price)
                .ToObservableCollection();
        }
        public async Task<ObservableCollection<CatalogBrand>> GetCatalogBrandAsync()
        {
            await Task.Delay(10);

            return MockCatalogBrand;
        }

        public async Task<ObservableCollection<CatalogType>> GetCatalogTypeAsync()
        {
            await Task.Delay(10);

            return MockCatalogType;
        }

    }
}