using Microsoft.AspNetCore.Mvc;
using Sibri.BLL.category.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sibri.WebPage.ViewComponents
{
    public class Product : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public Product(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int categoryid, int direction = 0)
        {
            var list = await _categoryService.GetCategoryListByID(categoryid, direction);
            return View(list.Skip(1).ToList());
        }
    }
}
