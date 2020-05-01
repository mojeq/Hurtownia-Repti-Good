using HurtowniaReptiGood.Models.Entities;
using HurtowniaReptiGood.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Services
{
    public class AdminService
    {
        private readonly MyContex _myContex;
        public AdminService(MyContex myContex)
        {
            _myContex = myContex;
        }
        public void AddNewProduct(NewProductViewModel newProduct)
        {
            ProductEntity newProductEntity = new ProductEntity
            {
                ProductName = newProduct.ProductName,
                ProductSymbol = newProduct.ProductSymbol,
                Stock = newProduct.Stock,
                Price = newProduct.Price,
                Photo = newProduct.Photo,
                Manufacturer = newProduct.Manufacturer,
            };
            _myContex.Products.Add(newProductEntity);
            _myContex.SaveChanges();
        }
    }
}
