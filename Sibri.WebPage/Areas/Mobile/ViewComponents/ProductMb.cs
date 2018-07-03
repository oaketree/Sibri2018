using Microsoft.AspNetCore.Mvc;
using Sibri.BLL.category.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sibri.WebPage.Areas.Mobile.ViewComponents
{
    public class ProductMb : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public ProductMb(ICategoryService categoryService) {
            this._categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int categoryid)
        {
            var productList = await _categoryService.GetProductCategoryListCn(categoryid);
            return View(productList.Skip(1).ToList());
        }
    }
}
