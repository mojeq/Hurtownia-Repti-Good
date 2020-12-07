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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MyContext _myContex;
        private readonly IAppService _appService;
        private ILogger _logger;

        public HomeController(
            UserManager<IdentityUser> userManager,
            MyContext myContex,
            IAppService appService,
            ILogger<HomeController> logger)
        {
            _userManager = userManager;
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
    }
}
