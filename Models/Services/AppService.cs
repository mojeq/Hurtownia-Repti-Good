using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HurtowniaReptiGood.Models.Entities;
using HurtowniaReptiGood.Models;

namespace HurtowniaReptiGood.Models
{
    public class AppService
    {
        private readonly MyContex _myContex;    
        public AppService(MyContex myContex)
        {
            _myContex = myContex;
        }

        // get current logged user
        public CustomerEntity GetLoggedCustomer(string userLogged)
        {
            CustomerEntity loggedUser = _myContex.Customers.FirstOrDefault(a => a.UserName == userLogged);
            return loggedUser;
        }

        // get list with all products from database
        public ProductsListViewModel GetAllProducts()
        {
            var productsList = new ProductsListViewModel();
            productsList.Products = _myContex.Products.Select(x=>new ProductViewModel
            {
                ProductId=x.ProductId,
                ProductSymbol=x.ProductSymbol,
                ProductName=x.ProductName,
                Price=x.Price,
                Stock=x.Stock,
                Photo=x.Photo,
                Manufacturer=x.Manufacturer
            }).ToList();
            return productsList;
        }

        // get products from manufcturer category
        public ProductsListViewModel GetProductsFromCategory(string manufacturer)
        {
            var productsList = new ProductsListViewModel();
            productsList.Products = _myContex.Products
                .Where(x => x.Manufacturer == manufacturer)
                .Select(x => new ProductViewModel
                {
                    ProductId = x.ProductId,
                    ProductSymbol = x.ProductSymbol,
                    ProductName = x.ProductName,
                    Price = x.Price,
                    Stock = x.Stock,
                    Photo = x.Photo
                }).ToList();

            return productsList;
        }
    }
}
