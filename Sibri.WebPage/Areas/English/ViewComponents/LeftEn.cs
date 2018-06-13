using Microsoft.AspNetCore.Mvc;
using Sibri.BLL.category.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sibri.WebPage.Areas.English.ViewComponents
{
    public class LeftEn : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public LeftEn(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string view, int categoryid, int direction = 0)
        {
            if (view == "Product")
            {
                var productList = await _categoryService.GetProductCategoryListEn(categoryid);
                return View(view, productList.Skip(1).ToList());
            }
            var list = await _categoryService.GetCategoryListByID(categoryid, direction);
            return View(view, list);

        }


    }
}
