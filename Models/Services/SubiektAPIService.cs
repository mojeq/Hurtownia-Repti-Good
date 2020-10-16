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

namespace HurtowniaReptiGood.Models.Services
{
    public class SubiektAPIService : ISubiektAPIService
    {
        private readonly MyContex _myContex;
        public SubiektAPIService(MyContex myContex)
        {
            _myContex = myContex;
        }

        // download products list from API SubiektGT
        public async Task<List<ProductAPI>> DownloadProductsStockFromSubiektGT()
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

            return productsListFromSubiektAPI;
        }

        // update products stock in database
        public async Task UpdateStockInDatabase(List<ProductAPI> productsListFromSubiektAPI)
        {
            foreach (ProductAPI product in productsListFromSubiektAPI)
            {
                ProductEntity productFromDatabase = await _myContex.Products.Where(x => x.IdSubiekt == product.IdSubiekt).FirstOrDefaultAsync();

                if(productFromDatabase is null)
                {
                    continue;
                }
                productFromDatabase.Stock = product.Stock;

                await _myContex.SaveChangesAsync();        
            }
        }
    }
}
