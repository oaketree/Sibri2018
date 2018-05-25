using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sibri.BLL.news.services;

namespace Sibri.WebPage.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService) {
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
            var news =await  _newsService.GetNews(id);
            return View(news);
        }

        
        public  IActionResult Search(string search)
        {
            return View((object)search);
        }


        public async Task<JsonResult> NewsPicList(int categoryid,int count,int language)
        {
            var result = await _newsService.GetPicNewsList(categoryid, count, language);
            return Json(result);
        }
        public async Task<JsonResult> NewsPicList2(int count, int language)
        {
            var result = await _newsService.GetPicNewsList(count, language);
            return Json(result);
        }

        public async Task<JsonResult> NewsList(int categoryid, int language, int pageSize, int pageIndex)
        {
            var result = await _newsService.GetNewsList(categoryid, language, pageSize, pageIndex);
            return Json(result);
        }

        public async Task<JsonResult> NewsList2(int count, int language)
        {
            var result = await _newsService.GetNewsList(count, language);
            return Json(result);
        }

        public async Task<JsonResult> NewsSearch(string keywords, int language, int pageSize, int pageIndex)
        {
            var result = await _newsService.GetNewsSearchList(keywords, language, pageSize, pageIndex);
            return Json(result);
        }


    }
}