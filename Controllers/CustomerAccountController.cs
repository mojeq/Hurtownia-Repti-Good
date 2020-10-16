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
        private readonly DpdService _dpdService;

        public CustomerAccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            MyContex myContex,
            AppService appService,
            CartService cartService,
            CustomerAccountService customerAccountService,
            DpdService dpdService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _myContex = myContex;
            _appService = appService;
            _cartService = cartService;
            _customerAccountService = customerAccountService;
            _dpdService = dpdService;
        }

        // view with orders list from logged customer
        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<IActionResult> OrdersHistory()
        {
            string userLogged = _userManager.GetUserName(HttpContext.User);

            OrderListViewModel ordersHistory;

            try
            {
                ordersHistory = await _customerAccountService.GetOrdersHistory(userLogged);
            }
            catch
            {
                throw new Exception("Nie udało się pobrać historii zamówień");
            }

            return View(ordersHistory);
        }

        // view with one order details
        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<IActionResult> OrderHistoryDetails(int orderId)
        {
            DpdTrackingStatusListViewModel dpdTrackingStatusViewModel;
            OrderAndOrderDetailListViewModel orderAndOrderDetails;

            try
            {
                dpdTrackingStatusViewModel = await _dpdService.GetTrackingStatusFromDPDWebservice(orderId);
            }
            catch
            {
                throw new Exception("Nie udało się pobrać śledzenia przesyłki z serwisu DPD");
            }

            try
            {
                orderAndOrderDetails = new OrderAndOrderDetailListViewModel()
                {
                    OrderDetails = await _cartService.GetCartDetailList(orderId),
                    Order = await _customerAccountService.GetOrder(orderId),
                };
            }
            catch
            {
                throw new Exception("Nie powiodło się pobranie zawartości koszyka lub zamówienia");
            }

            OrderAndOrderDetailListAndDpdTrackingStatusViewModel orderAndOrderDetailsAndTrackincStatus = new OrderAndOrderDetailListAndDpdTrackingStatusViewModel()
            {
                OrderAndDetailsOrder = orderAndOrderDetails,
                DpdTrackingStatusList = dpdTrackingStatusViewModel,
            };

            return View(orderAndOrderDetailsAndTrackincStatus);
        }
    }
}