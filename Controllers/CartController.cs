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
    public class CartController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly MyContex _myContex;
        private readonly AppService _appService;
        private readonly CartService _cartService;

        public CartController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            MyContex myContex,
            AppService appService,
            CartService cartService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _myContex = myContex;
            _appService = appService;
            _cartService = cartService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Cart(int orderId)
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
            var cartDetails = _cartService.GetCartDetailList(orderId);
            if (cartDetails == null)
            {
                return View("CartEmpty");
            }
            var invoiceAddress = _cartService.GetInvoiceAddress(orderId);
            var shippingAddress = _cartService.GetShippingAddress(orderId);            
            
            var dataToView = new CartAndAddressesViewModel()
            {
                ShippingAddress = shippingAddress,
                InvoiceAddress = invoiceAddress,
                CartToView= new OrderDetailListViewModel()
                {
                    OrderDetailList = cartDetails.OrderDetailList
                }
            };

            //string stateOrder;
            //Customer loggedUser;
            //Address invoiceAddress;
            //Address shippingAddress;
            //List<OrderDetail> itemsInCart = new List<OrderDetail>();

            // achive shipping address and invoice address from database
            

            //using(var contex = new MyContex())
            //{
            //    var order = contex.Orders.FirstOrDefault(a => a.OrderId == OrderId);
            //    stateOrder = order.StateOrder;
            //}

            //if (String.IsNullOrEmpty(Request.Cookies["cartStatus"]) || (stateOrder=="bought"))
            //{
            //    return View("CartEmpty");
            //}

            //using (var contex = new MyContex())
            //{
            //    loggedUser = contex.Customers.FirstOrDefault(a => a.UserName == userLogged);

            //    invoiceAddress = contex.Addresses.FirstOrDefault(b => b.AddressId == loggedUser.InvoiceAddressId);

            //    shippingAddress = contex.Addresses.FirstOrDefault(c => c.AddressId == loggedUser.ShippingAddressId);

            //    ViewBag.loggedUser = loggedUser;
            //    ViewBag.invoiceAddress = invoiceAddress;
            //    ViewBag.shippingAddress = shippingAddress;
            //    ViewBag.userLogged = loggedUser.UserName;

            //    itemsInCart = contex.OrderDetails.Where(c => c.OrderId == OrderId).ToList();
            //    ViewBag.itemCartList = itemsInCart;
            //}
            
            return View(dataToView);
        }

        [Authorize]
        public ActionResult AddItemToCart(ItemCartViewModel itemCart)
        {
            string userLogged = _userManager.GetUserName(HttpContext.User);
            CustomerEntity loggedUser = _appService.GetLoggedCustomer(userLogged);

            int orderId;
            if (String.IsNullOrEmpty(Request.Cookies["cartStatus"]))
            {
                orderId=_cartService.CreateNewCartOrder(loggedUser, itemCart);
            }
            else
            {
                orderId=_cartService.AddItemToExistCart(loggedUser, itemCart);
            }
            // CookieOptions option = new CookieOptions();
            // option.Expires = DateTime.Now.AddMinutes(30);
            Response.Cookies.Append("cartStatus", "tempCart");            
            //option.Expires = DateTime.Now.AddMinutes(30);
            Response.Cookies.Append("orderId", orderId.ToString());    
            //ViewBag.itemCartList = itemsInCart;
            return RedirectToAction("Cart", orderId);
        }

        //update quantity of item in cart
        [Authorize]
        [HttpPost]
        public ActionResult UpdateQuantityInCart(OrderDetailViewModel orderDetail)
        {
            _cartService.UpdateQuantityItemInCart(orderDetail.OrderDetailId, orderDetail.Quantity);

            return RedirectToAction("Cart", orderDetail.OrderId);
        }       

        [Authorize]
        public IActionResult RemoveItemFromCart(int orderId, int orderDetailId)
        {
            _cartService.RemoveItemFromCart(orderDetailId);
            return RedirectToAction("Cart", orderId);
        }
    }
}