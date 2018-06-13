using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sibri.BLL.pages.services;

namespace Sibri.WebPage.Areas.English.Controllers
{
    [Area("English")]
    public class ProductController : Controller
    {
        private readonly IPageService _pageService;

        public ProductController(IPageService pageService)
        {
            this._pageService = pageService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List(int id)
        {
            var page = await _pageService.GetPage(id, 1);
            return View(page);
        }
    }
}