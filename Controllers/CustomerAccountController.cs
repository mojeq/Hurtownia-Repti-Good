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
using HurtowniaReptiGood.Models.ViewModels;
using HurtowniaReptiGood.Models.Services;

namespace HurtowniaReptiGood.Controllers
{
    public class CustomerAccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly MyContex _myContex;
        private readonly AppService _appService;
        private readonly CartService _cartService;
        private readonly CustomerAccountService _customerAccountService;

        public CustomerAccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            MyContex myContex,
            AppService appService,
            CartService cartService,
            CustomerAccountService customerAccountService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _myContex = myContex;
            _appService = appService;
            _cartService = cartService;
            _customerAccountService = customerAccountService;
        }

        // view with orders list from logged customer
        [Authorize(Roles = "user")]
        [HttpGet]
        public IActionResult OrdersHistory()
        {
            string userLogged = _userManager.GetUserName(HttpContext.User);

            OrderListViewModel ordersHistory = _customerAccountService.GetOrdersHistory(userLogged);

            return View(ordersHistory);
        }

        // view with one order details
        [Authorize(Roles = "user")]
        [HttpGet]
        public IActionResult OrderHistoryDetails(int orderId)
        {            
            OrderAndOrderDetailListViewModel orderAndOrderDetails = new OrderAndOrderDetailListViewModel();
            orderAndOrderDetails.OrderDetails= _cartService.GetCartDetailList(orderId);
            orderAndOrderDetails.Order = _customerAccountService.GetOrder(orderId);

            return View(orderAndOrderDetails);
        }
    }
}