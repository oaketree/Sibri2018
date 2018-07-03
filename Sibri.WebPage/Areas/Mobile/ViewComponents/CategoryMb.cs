using Microsoft.AspNetCore.Mvc;
using Sibri.BLL.category.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sibri.WebPage.Areas.Mobile.ViewComponents
{
    public class CategoryMb : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public CategoryMb(ICategoryService categoryService) {
            this._categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string view,int categoryid,int direction)
        {
            var categorylist = await _categoryService.GetCategoryListByID(categoryid,direction);
            return View(view,categorylist.ToList());
        }

    }
}
