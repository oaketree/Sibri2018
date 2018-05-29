using Microsoft.AspNetCore.Mvc;
using Sibri.BLL.category.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sibri.WebPage.ViewComponents
{
    public class Left : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public Left(ICategoryService categoryService) {
            this._categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string view, int categoryid, int direction = 0)
        {
            if (view == "Product")
            {
                var productList = await _categoryService.GetProductCategoryListCn(categoryid);
                return View(view, productList.Skip(1).ToList());
            }
            var list = await _categoryService.GetCategoryListByID(categoryid, direction);
            return View(view, list);

        }

        
    }
}
