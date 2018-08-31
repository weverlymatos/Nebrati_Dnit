using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Dnit.Core.Models.Catalog;
using Dnit.Core.Services.RequestProvider;
using Dnit.Core.Extensions;
using System.Collections.Generic;
using Dnit.Core.Services.FixUri;
using Dnit.Core.Helpers;
using Dnit.Core.Services.Settings;
using System.Linq;
namespace Dnit.Core.Services.Catalog
{
    public class CatalogService : ICatalogService
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IFixUriService _fixUriService;

        private const string ApiUrlBase = "api/v1/c/catalog";

        public CatalogService(IRequestProvider requestProvider, IFixUriService fixUriService)
        {
            _requestProvider = requestProvider;
            _fixUriService = fixUriService;
        }

        public async Task<ObservableCollection<CatalogItem>> FilterAsync(string productfilter)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayCatalogEndpoint, $"{ApiUrlBase}/items/productfilter/{productfilter}");

            CatalogRoot catalog = await _requestProvider.GetAsync<CatalogRoot>(uri);

            if (catalog?.Data != null)
                return catalog?.Data.OrderBy(x=>x.Price).ToObservableCollection();
            else
                return new ObservableCollection<CatalogItem>();
        }

        public async Task<ObservableCollection<CatalogItem>> GetCatalogAsync()
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayCatalogEndpoint, $"{ApiUrlBase}/items");

            CatalogRoot catalog = await _requestProvider.GetAsync<CatalogRoot>(uri);

            if (catalog?.Data != null)
            {
                _fixUriService.FixCatalogItemPictureUri(catalog?.Data);
                return catalog?.Data.OrderBy(x=>x.Price).ToObservableCollection();
            }
            else
                return new ObservableCollection<CatalogItem>();
        }

        public async Task<ObservableCollection<CatalogBrand>> GetCatalogBrandAsync()
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayCatalogEndpoint, $"{ApiUrlBase}/catalogbrands");

            IEnumerable<CatalogBrand> brands = await _requestProvider.GetAsync<IEnumerable<CatalogBrand>>(uri);

            if (brands != null)
                return brands?.ToObservableCollection();
            else
                return new ObservableCollection<CatalogBrand>();
        }

        public async Task<ObservableCollection<CatalogType>> GetCatalogTypeAsync()
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayCatalogEndpoint, $"{ApiUrlBase}/catalogtypes");

            IEnumerable<CatalogType> types = await _requestProvider.GetAsync<IEnumerable<CatalogType>>(uri);

            if (types != null)
                return types.ToObservableCollection();
            else
                return new ObservableCollection<CatalogType>();
        }
    }
}
