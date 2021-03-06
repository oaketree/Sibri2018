﻿using Cms.BLL.category.viewmodels;
using Cms.Contract.category;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.category.services
{
    public interface ICategoryService
    {
        Task addNodes(int? parentid, string categoryname, string categorynameen, int? sortid, string href, bool ishref);
        Task<List<CategoryView>> categoryList();
        Task<List<Category>> getCategoryByID(int? id);
        Task<List<SelectListItem>> getSelectListItemByID(int? id);
        //Task<Category> getCategoryNameByID(int id);
        Task<Category> getNode(int categoryid);
        Task updateNode(int categoryid, string categoryname, string categorynameen, int? sortid, string href, bool ishref);
        Task<bool> delNode(int categoryid);

    }
}
