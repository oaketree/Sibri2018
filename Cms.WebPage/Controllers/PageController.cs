using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.BLL.pages.service;
using Cms.BLL.pages.viewmodels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cms.WebPage.Controllers
{
    public class PageController : Controller
    {
        private readonly IPageService _pageServices;
        public PageController(IPageService pageServices)
        {
            this._pageServices = pageServices;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(PageView pv)
        {
            if (ModelState.IsValid)
            {
                await _pageServices.AddPages(pv);
                return RedirectToAction(nameof(Create));
            }
            return View(pv);
        }

        [Authorize]
        public async Task<JsonResult> PageListAxios([FromBody] Dictionary<string, string> param)
        {
            var result = await _pageServices.GetPageList(int.Parse(param["pageSize"]), int.Parse(param["pageIndex"]), param["keywords"]);
            return Json(result);
        }

        [Authorize]
        public async Task DelPageAxios(int id)
        {
            await _pageServices.DelPage(id);
        }

        [Authorize]
        public async Task<JsonResult> Edit(int id)
        {
            var newsView = await _pageServices.GetPageByID(id);
            return Json(newsView);
        }

        [Authorize]
        public async Task DelPageImg(int id)
        {
            await _pageServices.DelPageImg(id);
        }

        public async Task UpdatePage(string pageid, string column, string language, string title, string content, bool picpage)
        {
            await _pageServices.UpdatePage(int.Parse(pageid), int.Parse(column), int.Parse(language), title,content, picpage);
        }
    }
}