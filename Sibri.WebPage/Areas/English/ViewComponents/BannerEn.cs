using Microsoft.AspNetCore.Mvc;
using Sibri.BLL.category.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sibri.WebPage.Areas.English.ViewComponents
{
    public class BannerEn : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public BannerEn(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int categoryid)
        {
            var category = await _categoryService.GetParentByID(categoryid);
            if (category != null)
                return View((object)category.CategoryNameEN);
            else
                return View("categoryID not exist");
        }
    }
}
