using Catalog.API.Infrastructure;
using Catalog.API.Models;
using global::Catalog.API.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Catalog.API.Migrations
{
    public class CatalogContextSeed
    {
        public async Task SeedAsync(CatalogContext context, IHostingEnvironment env, IOptions<CatalogSettings> settings, ILogger<CatalogContextSeed> logger)
        {
            //var policy = CreatePolicy(logger, nameof(CatalogContextSeed));

            //await policy.ExecuteAsync(async () =>
            //{
            //    var useCustomizationData = settings.Value.UseCustomizationData;
            //    var contentRootPath = env.ContentRootPath;
            //    var picturePath = env.WebRootPath;

            //    if (!context.CatalogBrands.Any())
            //    {
            //        await context.CatalogBrands.AddRangeAsync(useCustomizationData
            //            ? GetCatalogBrandsFromFile(contentRootPath, logger)
            //            : GetPreconfiguredCatalogBrands());

            //        await context.SaveChangesAsync();
            //    }

            //    if (!context.CatalogTypes.Any())
            //    {
            //        await context.CatalogTypes.AddRangeAsync(useCustomizationData
            //            ? GetCatalogTypesFromFile(contentRootPath, logger)
            //            : GetPreconfiguredCatalogTypes());

            //        await context.SaveChangesAsync();
            //    }

            //    if (!context.CatalogItems.Any())
            //    {
            //        await context.CatalogItems.AddRangeAsync(useCustomizationData
            //            ? GetCatalogItemsFromFile(contentRootPath, context, logger)
            //            : GetPreconfiguredItems());

            //        await context.SaveChangesAsync();

            //        GetCatalogItemPictures(contentRootPath, picturePath);
            //    }
            //});
        }

      
        private CatalogBrand CreateCatalogBrand(string brand)
        {
            brand = brand.Trim('"').Trim();

            if (String.IsNullOrEmpty(brand))
            {
                throw new Exception("catalog Brand Name is empty");
            }

            return new CatalogBrand
            {
                Brand = brand,
            };
        }

        private IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
        {
            return new List<CatalogBrand>()
            {
                new CatalogBrand() { Brand = "HP"},
                new CatalogBrand() { Brand = "Accer" },
                
            };
        }

     

        private CatalogType CreateCatalogType(string type)
        {
            type = type.Trim('"').Trim();

            if (String.IsNullOrEmpty(type))
            {
                throw new Exception("catalog Type Name is empty");
            }

            return new CatalogType
            {
                Type = type,
            };
        }

        private IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
        {
            return new List<CatalogType>()
            {
                new CatalogType() { Type = "Computador"},
                new CatalogType() { Type = "Notebook" },
                new CatalogType() { Type = "Acessórios" },

            };
        }
      
        private IEnumerable<CatalogItem> GetPreconfiguredItems()
        {
            return new List<CatalogItem>()
            {
                new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 2,  Description = "Produto1", Name = "Produto1", Price = 19.5M, PictureFileName = "fake_product_01.png" },
                new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 2,  Description = "Produto2", Name = "Produto2", Price= 8.50M, PictureFileName = "fake_product_02.png" },
                new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 5,  Description = "Produto3", Name = "Produto3", Price = 12, PictureFileName = "fake_product_03.png" },
                new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 2,  Description = "Produto4", Name = "Produto4", Price = 12, PictureFileName = "fake_product_04.png" },
                new CatalogItem { CatalogTypeId = 3, CatalogBrandId = 5,  Description = "Produto5", Name = "Produto5", Price = 8.5M, PictureFileName = "fake_product_05.png" },


            };
        }

        //private Microsoft.AspNetCore.Authorization.Policy CreatePolicy(ILogger<CatalogContextSeed> logger, string prefix, int retries = 3)
        //{
        //    return Microsoft.AspNetCore.Authorization.Policy.Handle<SqlException>().
        //        WaitAndRetryAsync(
        //            retryCount: retries,
        //            sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
        //            onRetry: (exception, timeSpan, retry, ctx) =>
        //            {
        //                logger.LogTrace($"[{prefix}] Exception {exception.GetType().Name} with message ${exception.Message} detected on attempt {retry} of {retries}");
        //            }
        //        );
        //}
    }
}
