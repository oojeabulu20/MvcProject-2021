using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcShoes.Models;
using MvcShoes.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace MvcShoes.Controllers
{
   
    public class SecurityController : Controller
    {

        private readonly UserManager<ShoesIdentityUser> userManager;
        private readonly RoleManager<ShoesIdentityRole> roleManager;
        private readonly SignInManager<ShoesIdentityUser> signinManager;

        public SecurityController(UserManager<ShoesIdentityUser> userManager, RoleManager<ShoesIdentityRole> roleManager,
            SignInManager<ShoesIdentityUser> signinManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signinManager = signinManager;
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(SignIn obj)
        {
            if (ModelState.IsValid)
            {
                var result = signinManager.PasswordSignInAsync
                (obj.UserName, obj.Password,
                    obj.RememberMe, false).Result;

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Shoes");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid user details");
                }
            }
            return View(obj);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult SignOut()
        {
            signinManager.SignOutAsync().Wait();
            return RedirectToAction("SignIn", "Security");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Register obj)
        {
            if (ModelState.IsValid)
            {
                if (!roleManager.RoleExistsAsync("Manager").Result)
                {
                    ShoesIdentityRole role = new ShoesIdentityRole();
                    role.Name = "Manager";
                    IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
                }

                ShoesIdentityUser user = new ShoesIdentityUser();
                user.UserName = obj.UserName;
                user.Email = obj.Email;
                user.FullName = obj.FullName;
                user.BirthDate = obj.BirthDate;

                IdentityResult result = userManager.CreateAsync
                (user, obj.Password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Manager").Wait();
                    return RedirectToAction("SignIn", "Security");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid user details");
                }
            }
            return View(obj);
        }
    }
}
