using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainLayer.Identity;
using KODOTIFront.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KODOTIFront.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            var result=await _signInManager.PasswordSignInAsync(
                model.Email, 
                model.Password,
                model.RememberMe, 
                false);
            if (result.Succeeded)
            {
                return Redirect("~/");
            }
            return View();
        }
        public IActionResult NewUser()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Create(NewUserViewModel model)
        {
            var result =await _userManager.CreateAsync(new ApplicationUser
            {
                UserName=model.Email,
                Email = model.Email,
            },model.Password) ;
            return View(result.Succeeded);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }
    }
}