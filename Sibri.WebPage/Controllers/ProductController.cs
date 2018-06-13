using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sibri.BLL.pages.services;

namespace Sibri.WebPage.Controllers
{
    public class ProductController : Controller
    {
        private readonly IPageService _pageService;

        public ProductController(IPageService pageService) {
            this._pageService = pageService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List(int id)
        {
            var page = await _pageService.GetPage(id,0);
            return View(page);
        }
    }
}