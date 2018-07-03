using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sibri.BLL.news.services;

namespace Sibri.WebPage.Areas.Mobile.Controllers
{
    [Area("Mobile")]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            this._newsService = newsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List(int id)
        {
            return View((int)id);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var news = await _newsService.GetNews(id);
            return View(news);
        }
        public IActionResult Search(string search)
        {
            return View((object)search);
        }
    }
}