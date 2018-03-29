using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.BLL.login.services;
using Cms.Contract.login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cms.WebPage.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminServices _adminServices;

        public AdminController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Admins admin)
        {
            if (ModelState.IsValid)
            {
                var user = await _adminServices.getUser(admin);
                if (user != null){
                    HttpContext.Session.SetString("userid",admin.UserName);
                    ModelState.AddModelError("", "正确");
                }
                else {
                    ModelState.AddModelError("", "用户名或密码错误！");
                }
            }
            return View();
        }

    }
}