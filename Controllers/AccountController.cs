﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HurtowniaReptiGood.Controllers
{
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private ILogger _logger;

        public AccountController(
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<HomeController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return RedirectToAction("Login", "Account");
        }

        // logging to app
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if(username == null || password == null)
            {
                return RedirectToAction("Login");
            }

            var user = await _userManager.FindByNameAsync(username);            

            if (user != null)
            {
                Response.Cookies.Append("userLogged", user.UserName);

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
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            TempData["LogInfo"] = "Niepoprawny login lub hasło.";

            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            if (!String.IsNullOrEmpty((string)TempData["LogInfo"]))
            {
                ViewBag.LogInfo = TempData["LogInfo"];
            }

            return View();
        }

        // registering new user
        [Authorize(Roles = "admin")]
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

        //change password
        public IActionResult ChangePassword()
        {
            if (!String.IsNullOrEmpty((string)TempData["LogInfo"]))
            {
                ViewBag.LogInfo = TempData["LogInfo"];
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            try
            {
                await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
                TempData["LogInfo"] = "Zmieniono hasło.";
            }
            catch (Exception e)
            {
                _logger.LogInformation(e,"Zmiana hasła nieudana {user}", user);
                TempData["LogInfo"] = "Zmiana hasła niudana.";
            }            

            return RedirectToAction("ChangePassword");
        }
    }
}
