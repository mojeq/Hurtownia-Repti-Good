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
using FluentAssertions.Common;
using HurtowniaReptiGood.Models.Interfaces;

namespace HurtowniaReptiGood.Controllers
{
    public class CartController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly MyContext _myContex;
        private readonly IAppService _appService;
        private readonly ICartService _cartService;

        public CartController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            MyContext myContex,
            IAppService appService,
            ICartService cartService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _myContex = myContex;
            _appService = appService;
            _cartService = cartService;
        }

        // view current cart with added items
        [Authorize (Roles="user")]
        [HttpGet]
        public async Task<ActionResult> Cart(int orderId)
        {
            if (orderId == 0)
            {
                if (String.IsNullOrEmpty(Request.Cookies["cartStatus"]) && String.IsNullOrEmpty(Request.Cookies["orderID"]))
                {
                    return View("CartEmpty");
                }
                else
                {
                    orderId = Int16.Parse(Request.Cookies["orderID"]);
                }
            }

            string userLogged = _userManager.GetUserName(HttpContext.User);        
            ViewBag.userLogged = userLogged;
           
            //preparing addresses and cart to View
            var cartDetails = await _cartService.GetCartDetailList(orderId);
            if (cartDetails.OrderDetailList.Count == 0)
            {
                return View("CartEmpty");
            }
            var invoiceAddress = await _cartService.GetInvoiceAddress(orderId);
            var shippingAddress = await _cartService.GetShippingAddress(orderId);            
            
            var dataToView = new CartAndAddressesViewModel()
            {
                ShippingAddress = shippingAddress,
                InvoiceAddress = invoiceAddress,
                CartToView= new OrderDetailListViewModel()
                {
                    OrderDetailList = cartDetails.OrderDetailList
                }
            };    

            return View(dataToView);
        }

        // adding next product to cart
        [Authorize(Roles = "user")]
        public async Task<ActionResult> AddItemToCart(ItemCartViewModel itemCart)
        {
            string userLogged = _userManager.GetUserName(HttpContext.User);
            CustomerEntity loggedUser = await _appService.GetLoggedCustomer(userLogged);

            int orderId;
            if (String.IsNullOrEmpty(Request.Cookies["cartStatus"]))
            {
                orderId = await _cartService.CreateNewCartOrder(loggedUser, itemCart);

                Response.Cookies.Append("orderId", orderId.ToString());

                Response.Cookies.Append("cartStatus", "tempCart");
            }
            else
            {
                string orderIdCookie = Request.Cookies["orderId"];

                orderId = Int16.Parse(orderIdCookie);
                await _cartService.AddItemToExistCart(orderId, itemCart);
            }
       
            //Response.Cookies.Append("orderId", orderId.ToString());    
          
            return RedirectToAction("Cart", orderId);
        }

        // update quantity of item in cart
        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<ActionResult> UpdateQuantityInCart(OrderDetailViewModel orderDetail)
        {
            await _cartService.UpdateQuantityItemInCart(orderDetail.OrderDetailId, orderDetail.Quantity);

            return RedirectToAction("Cart", orderDetail.OrderId);
        }

        // deleting one item from current cart
        [Authorize(Roles = "user")]
        public async Task<IActionResult> RemoveItemFromCart(int orderId, int orderDetailId)
        {
            await _cartService.RemoveItemFromCart(orderDetailId);

            return RedirectToAction("Cart", orderId);
        }

        // save new order to database exactly change state of current order and create attachment and sending mail with confirmation order
        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<IActionResult> SaveNewOrder(int orderId, double valueOrder)
        {
            await _cartService.SaveNewOrder(orderId, valueOrder, "erer");

            await _cartService.CreatePdfAttachmentWithOrder(orderId);

            await _cartService.SendMailWithAttachment(orderId);

            Response.Cookies.Delete("cartStatus");

            Response.Cookies.Delete("orderId");

            return View();
        }
    }
}