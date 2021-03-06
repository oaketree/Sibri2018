﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sibri.BLL.pages.services;

namespace Sibri.WebPage.Areas.Mobile.Controllers
{
    [Area("Mobile")]
    public class PageController : Controller
    {
        private readonly IPageService _pageService;
        public PageController(IPageService pageService)
        {
            this._pageService = pageService;
        }


        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> PageInfo(int id)
        {
            var page = await _pageService.GetPage(id, 0);
            return View(page);
        }
    }
}