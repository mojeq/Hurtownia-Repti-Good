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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using HurtowniaReptiGood.Models.Interfaces;

namespace HurtowniaReptiGood.Controllers
{
    public class HomeController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly MyContext _myContex;
        private readonly IAppService _appService;
        private ILogger _logger;

        public HomeController(
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            MyContext myContex,
            IAppService appService,
            ILogger<HomeController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _myContex = myContex;
            _appService = appService;
            _logger = logger;
        }

        // view list with all products from database 
        [Authorize (Roles ="user")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userLogged = _userManager.GetUserName(HttpContext.User);

            var productsList = await _appService.GetAllProducts();     
            
            return View(productsList);
        }

        // view list with products from selected category
        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<IActionResult> ShowProductsFromCategory(string productCategory)
        {
            if(productCategory == "wszyscy")
            {
                return RedirectToAction("Index");
            }

            var productsList = await _appService.GetProductsFromCategory(productCategory);

            return View("Index", productsList);
        }

        // view site with terms and conditions 
        [Authorize (Roles = "user")]
        [HttpGet]
        public IActionResult TermsAndConditions()
        {
            return View();
        }

        [Authorize (Roles = "user")]
        public IActionResult Contact()
        {
            return View();
        }


        // logging to app
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            Response.Cookies.Append("userLogged", user.UserName);

            if (user != null)
            {
                //Sign in
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (signInResult.Succeeded)
                {                    
                    var roleType = await _userManager.GetRolesAsync(user);

                    _logger.LogInformation("Zalogowano do systemu: " + roleType[0] + ", " + username);

                    if (roleType[0] == "admin")
                    {
                        return RedirectToAction("Orders", "Admin");
                    }
                    else if (roleType[0] == "user")
                    {
                        return RedirectToAction("Index");
                    }                  
                }
            }
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }

        // registering new user
        [Authorize (Roles = "admin")]
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

        [Authorize(Roles = "admin")]
        public IActionResult Register()
        {
            return View();
        }

        // log out from app
        public async Task<IActionResult> LogOut()
        {
            Response.Cookies.Delete("cartStatus");

            Response.Cookies.Delete("orderId");

            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}
