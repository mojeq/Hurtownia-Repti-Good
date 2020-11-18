using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HurtowniaReptiGood.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HurtowniaReptiGood.Models.Services;
using HurtowniaReptiGood.Models;
using HurtowniaReptiGood.Models.Interfaces;

namespace HurtowniaReptiGood.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IAppService _appService;
        private readonly ICustomerAccountService _customerAccountService;
        private readonly IDpdService _dpdService;
        private readonly ISubiektAPIService _subiektAPIService;
        public AdminController(IAdminService adminService, IAppService appService, 
            ICustomerAccountService customerAccountService, IDpdService dpdService, ISubiektAPIService subiektAPIService)
        {
            _adminService = adminService;
            _appService = appService;
            _customerAccountService = customerAccountService;
            _dpdService = dpdService;
            _subiektAPIService = subiektAPIService;
        }

        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View();
        }

        //adding new product to database
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddNewProduct(NewProductViewModel newProduct)
        {
            await _adminService.AddNewProduct(newProduct);

            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult AddNewProduct()
        {
            return View();
        }

        // view list with all products
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Products()
        {
            var productsList = await _appService.GetAllProducts();

            return View(productsList);
        }

        // view details of one product
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> ProductDetails(int productId)
        {
            ProductViewModel productToEdit = await _adminService.GetProduct(productId);

            return View("EditProduct", productToEdit);
        }

        // saving edited product and return to view with all products
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductViewModel productToChange)
        {
            await _adminService.SaveChangesProduct(productToChange);

            return RedirectToAction("Products");
        }

        // view list with all orders
        [Authorize(Roles ="admin")]
        [HttpGet]
        public async Task<IActionResult> Orders()
        {
            var orders = await _adminService.GetOrders();

            return View(orders);
        }

        // view details of one order
        [Authorize(Roles ="admin")]
        [HttpGet]
        public async Task<IActionResult> OrderDetails(int orderId)
        {
            OrderDetailsAndDpdTrackingStatusViewModel orderDetailsAndTracking = new OrderDetailsAndDpdTrackingStatusViewModel()
            {
                DpdTrackingStatusList = await _dpdService.GetTrackingStatusFromDPDWebservice(orderId),
                OrderDetails = await _adminService.GetOrderDetails(orderId),
            };

            return View(orderDetailsAndTracking);
        }

        // edit item in order
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> EditOrderDetail(int orderDetailId)
        {
            OrderDetail orderDetail = await _adminService.GetOrderDetail(orderDetailId);

            return View(orderDetail);
        }

        // create view with order to edit
        [Authorize(Roles = "admin")]
        [HttpGet]        
        public async Task<IActionResult> EditOrder(int orderId)
        {
            OrderViewModel orderToEdit = await _customerAccountService.GetOrder(orderId);

            return View(orderToEdit);
        }

        // edit order, change status, add tracking number etc
        [Authorize(Roles = "admin")]
        [HttpPost]        
        public async Task<IActionResult> EditOrder(OrderViewModel order)
        {
            await _adminService.SaveChangesOrder(order);

            return RedirectToAction("Orders");         
        }

        // update products stock form SubiektGT via API
        [Authorize(Roles ="admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateProductsStock()
        {
            UpdateProductsStockStatusViewModel status = new UpdateProductsStockStatusViewModel();

            try
            {
                await _subiektAPIService.DownloadAndUpdateProductsStockFromSubiektGT();                

                status.UpdateStatus = "Import stanu magazynowego powiódł się.";
            }
            catch
            {                
                status.UpdateStatus = "Import stanu magazynowego nie powiódł się.";
            }     

            return View("UpdateStock", status);
        }

        [Authorize(Roles = "admin")]        
        public IActionResult UpdateStock()
        {
            UpdateProductsStockStatusViewModel status = new UpdateProductsStockStatusViewModel();

            status.UpdateStatus = "";

            return View(status);
        }
    }
}