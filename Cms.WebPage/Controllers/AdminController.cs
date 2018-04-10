using Cms.BLL.login.services;
using Cms.BLL.login.viewmodels;
using Cms.Contract.login;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cms.WebPage.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminServices _adminServices;

        public AdminController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }
        public IActionResult Login(string returnUrl = null)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                RedirectToAction(nameof(HomeController.Index), "Home");
            }
            TempData["returnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminView admin, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = await _adminServices.getUser(admin.UserName, admin.Password);
                if (user != null)
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));
                    if (admin.isPersistent)
                    {
                        await HttpContext.SignInAsync(
                              CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = true });
                    }
                    else
                    {
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                    }
                    if (returnUrl == null)
                    {
                        returnUrl = TempData["returnUrl"]?.ToString();
                    }
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    //HttpContext.Session.SetString("userid",admin.UserName);
                    //HttpContext.Session.Set<Admins>("userid", admin);
                }
                else
                {
                    const string badUserNameOrPasswordMessage = "用户名或密码错误！";
                    //return BadRequest(badUserNameOrPasswordMessage);
                    ModelState.AddModelError("", badUserNameOrPasswordMessage);
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Admin");
        }

        public IActionResult Denied()
        {
            return View();
        }

        [Authorize]
        public IActionResult AdminList()
        {
            return View();
        }
        [Authorize]
        public async Task<bool> AddUser(string username, string password)
        {
            return await _adminServices.addUser(username, password);
        }
        [Authorize]
        public async Task<JsonResult> UserList()
        {
            var result= await _adminServices.userList();
            return Json(result);
        }


    }
}