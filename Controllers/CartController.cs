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

namespace HurtowniaReptiGood.Controllers
{
    public class CartController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly MyContex _myContex;
        private readonly AppService _appService;

        public CartController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            MyContex myContex,
            AppService appService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _myContex = myContex;
            _appService = appService;
        }
        public ActionResult AddItemToCart(ItemCartViewModel itemCart)
        {
            string userLogged = _userManager.GetUserName(HttpContext.User);           
            CustomerEntity loggedUser = _myContex.Customers.FirstOrDefault(a => a.UserName == userLogged);

            int orderId;
            if (String.IsNullOrEmpty(Request.Cookies["cartStatus"]))
            {
                orderId=_appService.CreateNewCartOrder(loggedUser);
            }
            else
            {
                orderId=_appService.AddItemToExistCart(loggedUser, itemCart);
            }

            // CookieOptions option = new CookieOptions();
            // option.Expires = DateTime.Now.AddMinutes(30);
            Response.Cookies.Append("cartStatus", "tempCart");
            //option.Expires = DateTime.Now.AddMinutes(30);
            Response.Cookies.Append("orderId", orderId.ToString());       
            
            //ViewBag.itemCartList = itemsInCart;
            return View(orderId);
        }
    }
}