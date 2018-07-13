using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sibri.WebPage.Areas.Mobile.Controllers
{
    [Area("Mobile")]
    public class StaticController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Job()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult History()
        {
            return View();
        }
        public IActionResult BriefIntroduction()
        {
            return View();
        }
        public IActionResult Platform()
        {
            return View();
        }
        public IActionResult Qualifications()
        {
            return View();
        }
        public IActionResult Equipment()
        {
            return View();
        }
        public IActionResult Service()
        {
            return View();
        }

    }
}