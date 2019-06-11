using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using HomeOwners_Exemption.Models;
using HomeOwners_Exemption.Services;

namespace HomeOwners_Exemption.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private IUserService _userService;
        private readonly homeownerContext _context;

        public AuthController(IUserService userService, homeownerContext context)
        {
            _context = context;
            _userService = userService;
        }
        [Route("signin")]
        public IActionResult SignIn()
        {
            return View(new SignInModel());
        }

        [Route("signout")]
        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [Route("signin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                Services.User user;
                if (await _userService.ValidateCredentials(model.Username, model.Password, _context, out user))
                {
                    await SignInUser(user.Username, user.UserFullName, user.Role, user.RoleId);
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
        public async Task SignInUser(string username, string fullname, string role, int roldId)
        {
            var claims = new List<System.Security.Claims.Claim> {
                new System.Security.Claims.Claim(ClaimTypes.NameIdentifier, username),
                new System.Security.Claims.Claim("name", username),
                new System.Security.Claims.Claim("Username", fullname),
                new System.Security.Claims.Claim("RoleTitle", role),
                new System.Security.Claims.Claim("RoleId", roldId.ToString()),
                new System.Security.Claims.Claim(ClaimTypes.Role, role)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", null);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
        }

    }
}