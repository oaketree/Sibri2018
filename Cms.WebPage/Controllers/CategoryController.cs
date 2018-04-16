using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.BLL.category.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cms.WebPage.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ICategoryService _categoryServices;
        public CategoryController(ICategoryService categoryService) {
            this._categoryServices = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task AddNodes(int? parentid, string categoryname, string categorynameen, int? sortid, string href = null, string hrefen = null,bool ishref=false)
        {
            await _categoryServices.addNodes(parentid, categoryname, categorynameen, sortid, href, hrefen,ishref);
            //return Json(result);
        }
        [Authorize]
        public async Task<JsonResult> NodeList()
        {
            var result = await _categoryServices.categoryList();
            return Json(result);
        }
        [Authorize]
        public async Task<JsonResult> GetNode(int categoryid)
        {
            var result = await _categoryServices.getNode(categoryid);
            return Json(result);
        }

        [Authorize]
        public async Task UpdateNode(int categoryid, string categoryname, string categorynameen, int? sortid, string href = null, string hrefen = null,bool ishref=false)
        {
            await _categoryServices.updateNode(categoryid,categoryname,categorynameen,sortid,href,hrefen,ishref);
        }
        [Authorize]
        public async Task<bool> DelNode(int categoryid) {
            var result= await _categoryServices.delNode(categoryid);
            return result;
        }
    }
}