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
        //public IEnumerable<ItemCartViewModel> AddItemToCart(ItemCartViewModel itemCart)
        //{
        //    IEnumerable<ItemCartViewModel> itemsInCart;
        //    CustomerViewModel userLogged;
        //    int orderId;
        //    return itemsInCart;
        //}
        public int CreateNewCartOrder(CustomerEntity loggedUser)
        {
            OrderEntity order = new OrderEntity()
            {
                CustomerId = loggedUser.CustomerId,
                StateOrder = "cart",
                DateOrder = DateTime.Now,
                StatusOrder = "W realizacji",
            };
            _myContex.Orders.Add(order);
            _myContex.SaveChanges();

            int orderId = _myContex.Orders.OrderByDescending(s => s.OrderId)
                                 .Where(o => o.CustomerId == loggedUser.CustomerId)
                                 .Where(o => o.StateOrder == "cart")
                                 .FirstOrDefault().OrderId;

            return orderId;
        }
        public int AddItemToExistCart(CustomerEntity loggedUser, ItemCartViewModel itemCart)
        {
            // finding last order ID
            int orderId = _myContex.Orders.OrderByDescending(s => s.OrderId)
                                             .Where(o => o.CustomerId == loggedUser.CustomerId)
                                             .Where(o => o.StateOrder == "cart")
                                             .FirstOrDefault().OrderId;

            //checking it is this item in cart if yes increase quantity if no, add new position OrderDetail
            OrderDetailEntity orderDetailExist = _myContex.OrderDetails.Where(a => a.OrderId == orderId)
                                                                  .Where(a => a.ProductSymbol == itemCart.ProductSymbol)
                                                                  .FirstOrDefault();
            if (orderDetailExist == null)
            {
                // create object with order details and save in database
                OrderDetailEntity orderDetail = new OrderDetailEntity()
                {
                    ProductSymbol = itemCart.ProductSymbol,
                    ProductName = itemCart.ProductName,
                    OrderId = orderId,
                    Quantity = Int16.Parse(itemCart.Quantity),
                    Price = itemCart.Price,
                };
                _myContex.OrderDetails.Add(orderDetail);
                _myContex.SaveChanges();
            }
            else
            {
                orderDetailExist.Quantity += int.Parse(itemCart.Quantity);
                _myContex.SaveChanges();
            }

            //receive items in cart
            var itemsInCart = _myContex.OrderDetails.Where(c => c.OrderId == orderId).ToList();
            return orderId;
        }
    }
}
