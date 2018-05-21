using Microsoft.AspNetCore.Mvc;
using Sibri.BLL.category.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sibri.WebPage.ViewComponents
{
    public class LeftViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public LeftViewComponent(ICategoryService categoryService) {
            this._categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string view,int categoryid, int direction=0)
        {
            var list = await _categoryService.GetCategoryListByID(categoryid, direction);
            return View(view,list);
        }
    }
}
