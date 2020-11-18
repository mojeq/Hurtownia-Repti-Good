using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HurtowniaReptiGood.Models;
using Newtonsoft.Json;
using HurtowniaReptiGood.Models.Entities;
using Microsoft.EntityFrameworkCore;
using HurtowniaReptiGood.Models.Interfaces;
using HurtowniaReptiGood.Models.Repositories;
using MimeKit.Encodings;
using HurtowniaReptiGood.Models.Interfaces.Repositories;

namespace HurtowniaReptiGood.Models.Services
{
    public class SubiektAPIService : ISubiektAPIService
    {
        private readonly IProductRepository _productRepository;
        private readonly MyContext _myContex;
        public SubiektAPIService(IProductRepository productRepository, MyContext myContex)
        {
            _productRepository = productRepository;
            _myContex = myContex;
        }

        // download products list from API SubiektGT
        public async Task DownloadAndUpdateProductsStockFromSubiektGT()
        {
            List<ProductAPI> productsListFromSubiektAPI = new List<ProductAPI>();

            SubiektAPI subiektAPI = new SubiektAPI();

            HttpClient client = await subiektAPI.InitAPI();

            HttpResponseMessage response = await client.GetAsync("api/Product");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                productsListFromSubiektAPI = JsonConvert.DeserializeObject<List<ProductAPI>>(result);
            }

            var productListToUpdate = await _productRepository.GetAllAsync();

            var resultList = from product1 in productsListFromSubiektAPI
                             join product2 in productListToUpdate
                             on product1.IdSubiekt equals product2.IdSubiekt
                             select new ProductEntity
                             {
                                 IdGroupSubiekt = product2.IdGroupSubiekt,
                                 IdSubiekt = product2.IdSubiekt,
                                 ProductId = product2.ProductId,
                                 ProductSymbol = product2.ProductSymbol,
                                 ProductName = product2.ProductName,
                                 Manufacturer = product2.Manufacturer,
                                 Price = product2.Price,
                                 Photo = product2.Photo,
                                 Stock = product1.Stock
                             };

            await _productRepository.UpdateRange(resultList);
        }
    }
}
