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
        public async Task<JsonResult> Edit(int id)
        {
            var newsView = await _newsServices.GetNewsByID(id);
            return Json(newsView);
        }

        [Authorize]
        public async Task DelNewsImg(int id)
        {
            await _newsServices.DelNewsImg(id);
        }

        //[HttpPost]
        //[Authorize]
        //public IActionResult Edit(NewsView nv)
        //{
        //    //var newsView = await _newsServices.GetNewsByID(id);
        //    return View();
        //}


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
                return RedirectToAction(nameof(Create));
                //return Content("<script>alert('发布成功！');window.close();</script>");
            }
            return View(nv);
        }
        //[Authorize]
        //public async Task<JsonResult> NewsList(int pageSize=10, int pageIndex = 1, string keywords = "", string category = "")
        //{
        //    var result = await _newsServices.GetNewsList(pageSize, pageIndex, keywords, category);
        //    return Json(result);
        //}
        [Authorize]
        public async Task<JsonResult> NewsListAxios([FromBody] Dictionary<string,string> param)
        {
            var result = await _newsServices.GetNewsList(int.Parse(param["pageSize"]), int.Parse(param["pageIndex"]), param["keywords"], param["category"]);
            return Json(result);
        }
        //[Authorize]
        //public async Task UpdateNews([FromBody] Dictionary<string, string> param)
        //{
        //    await _newsServices.UpdateNews(int.Parse(param["newsID"]),int.Parse(param["column"]), int.Parse(param["language"]), param["title"], param["subTitle"], param["content"]);
        //}
        public async Task UpdateNews2(string newsID,string column,string language,string title,string subTitle,string content,bool picnews)
        {
            //return newsID;
            await _newsServices.UpdateNews(int.Parse(newsID), int.Parse(column), int.Parse(language), title, subTitle, content, picnews);
        }

        [Authorize]
        public async Task DelNewsAxios(int id)
        {
            await _newsServices.DelNews(id);
        }
    }
}