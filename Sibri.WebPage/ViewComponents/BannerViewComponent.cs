using Microsoft.AspNetCore.Mvc;
using Sibri.BLL.category.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sibri.WebPage.ViewComponents
{
    public class BannerViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public BannerViewComponent(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int categoryid)
        {
            var category = await _categoryService.GetParentByID(categoryid);
            if (category != null)
                return View((object)category.CategoryName);
            else
                return View("分类ID不存在");
        }
    }
    
}
