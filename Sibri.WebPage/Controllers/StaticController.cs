using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sibri.WebPage.Controllers
{
    public class StaticController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BriefIntroduction()
        {
            return View();
        }

        public IActionResult History()
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

        public IActionResult Job()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Qualifications()
        {
            return View();
        }

    }
}