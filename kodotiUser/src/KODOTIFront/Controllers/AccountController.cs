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

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
    }
}