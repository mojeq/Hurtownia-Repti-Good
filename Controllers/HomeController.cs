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
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly MyContex _myContex;
        private readonly AppService _appService;

        public HomeController(
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

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            string userLogged = _userManager.GetUserName(HttpContext.User);
            ViewBag.userLogged = userLogged;

            var productsList = new ProductsListViewModel();
            productsList.Products = _appService.GetAllProducts();            
            return View(productsList);
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                //Sign in
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (signInResult.Succeeded)
                {
                    string userLogged = _userManager.GetUserName(HttpContext.User);
                    Response.Cookies.Append("userLogged", userLogged);
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            var user = new IdentityUser
            {
                UserName = username,
                Email = "",
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                //Sign in
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Register");
        }
        public async Task<IActionResult> LogOut()
        {
            Response.Cookies.Delete("cartStatus");
            Response.Cookies.Delete("orderId");
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}
