using Microsoft.AspNetCore.Mvc;
using Sibri.BLL.category.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sibri.WebPage.Areas.Mobile.ViewComponents
{
    public class TitleMb : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public TitleMb(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int categoryid)
        {
            var category = await _categoryService.GetCategoryByID(categoryid);
            return View(category);
        }
    }
}
