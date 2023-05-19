using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RedeAgro.Models;
using System.ComponentModel.DataAnnotations;

namespace RedeAgro.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<UserApp> _userManager;
        private SignInManager<UserApp> _signInManager;

        public AccountController(
            UserManager<UserApp> userManager,
            SignInManager<UserApp> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Required][EmailAddress] string email, [Required] string password, string returnurl= "")
        {
            if (ModelState.IsValid)
            {
                await _signInManager.SignOutAsync();
                //UserApp appUser = await _userManager.FindByEmailAsync(email);

                var result = await _signInManager.PasswordSignInAsync(
                    email, password, false, false);

                if (result.Succeeded)
                {
                    return Redirect(returnurl ?? "/");
                }

                ModelState.AddModelError(nameof(email), "Login Failed: Invalid Email or Password");

                //if (appUser != null)
                //{
                //    Microsoft.AspNetCore.Identity.SignInResult result =
                //        await _signInManager.PasswordSignInAsync(appUser, password, false, false);

                //    if (result.Succeeded)
                //    {
                //        return Redirect(returnurl ?? "/");
                //    }
                //}
            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
