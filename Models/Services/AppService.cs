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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly MyContex _myContex;    
        public AppService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            MyContex myContex)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _myContex = myContex;
        }
        public CustomerEntity GetLoggedCustomer(string userLogged)
        {
            CustomerEntity loggedUser = _myContex.Customers.FirstOrDefault(a => a.UserName == userLogged);
            return loggedUser;
        }

        public IEnumerable<ProductViewModel> GetAllProducts()
        {
            var productsList = _myContex.Products.Select(x=>new ProductViewModel
            {
                ProductId=x.ProductId,
                ProductSymbol=x.ProductSymbol,
                ProductName=x.ProductName,
                Price=x.Price,
                Stock=x.Stock,
                Photo=x.Photo
            }).ToList();
            return productsList;
        }
    }
}
