using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.BLL.category.services;
using Cms.BLL.news.service;
using Cms.BLL.news.viewmodels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;

namespace Cms.WebPage.Controllers
{
    public class NewsController : Controller
    {
        /// <summary>
        /// 新闻菜单所在的父ID
        /// </summary>
        //private const int categoryID = 8;
        private readonly INewsService _newsServices;

        public NewsController(INewsService newsService)
        {
            this._newsServices = newsService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Create()
        {

            return View();
        }

   
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(NewsView nv,string[] checkbox)
        {
            if (ModelState.IsValid)
            {
                await _newsServices.AddNews(nv, checkbox);
                //return Content();

            }
            return View(nv);
        }
        [Authorize]
        public async Task<JsonResult> NewsList(int pageSize, int pageIndex = 1, string keywords = null, string category = null)
        {

            var result = await _newsServices.GetNewsList(pageSize, pageIndex, keywords, category);
            return Json(result);
        }

    }
}