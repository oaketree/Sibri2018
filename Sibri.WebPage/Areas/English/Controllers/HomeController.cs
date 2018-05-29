using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sibri.WebPage.Areas.English.Controllers
{
    [Area("English")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}