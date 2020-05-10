using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HurtowniaReptiGood.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HurtowniaReptiGood.Models.Services;
using HurtowniaReptiGood.Models;

namespace HurtowniaReptiGood.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminService _adminService;
        private readonly AppService _appService;
        private readonly CustomerAccountService _customerAccountService;
        public AdminController(AdminService adminService, AppService appService, CustomerAccountService customerAccountService)
        {
            _adminService = adminService;
            _appService = appService;
            _customerAccountService = customerAccountService;
        }

        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddNewProduct(NewProductViewModel newProduct)
        {
            _adminService.AddNewProduct(newProduct);
            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult AddNewProduct()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Products()
        {
            var productsList = _appService.GetAllProducts();
            return View(productsList);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult ProductDetails(int productId)
        {
            ProductViewModel productToEdit = _adminService.GetProduct(productId);

            return View("EditProduct", productToEdit);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult EditProduct(ProductViewModel productToChange)
        {
            _adminService.SaveChangesProduct(productToChange);

            return RedirectToAction("Products");
        }

        [Authorize(Roles ="admin")]
        [HttpGet]
        public IActionResult Orders()
        {
            var orders = _adminService.GetOrders();

            return View(orders);
        }

        [Authorize(Roles ="admin")]
        [HttpGet]
        public IActionResult OrderDetails(int orderId)
        {
            var orderDetails = _adminService.GetOrderDetails(orderId);

            return RedirectToAction("Orders", orderDetails);
        }
    }
}