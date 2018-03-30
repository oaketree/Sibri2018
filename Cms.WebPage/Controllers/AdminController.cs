using Cms.BLL.login.services;
using Cms.BLL.login.viewmodels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
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
            TempData["returnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminView admin, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = await _adminServices.getUser(admin.UserName,admin.Password);
                if (user != null){
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
                        RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                    //HttpContext.Session.SetString("userid",admin.UserName);
                    //HttpContext.Session.Set<Admins>("userid", admin);
                    //ModelState.AddModelError("", "正确");
                }
                else {
                    const string badUserNameOrPasswordMessage = "用户名或密码错误！";
                    //return BadRequest(badUserNameOrPasswordMessage);
                    ModelState.AddModelError("", badUserNameOrPasswordMessage);
                }
            }
            return View();
        }

    }
}