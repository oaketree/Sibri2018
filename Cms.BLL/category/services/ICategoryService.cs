using Cms.BLL.category.viewmodels;
using Cms.Contract.category;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.category.services
{
    public interface ICategoryService
    {
        Task addNodes(int? parentid, string categoryname, string categorynameen, int? sortid, string href = null, string hrefen = null,bool ishref = false);
        Task<List<CategoryView>> categoryList();
        Task<List<Category>> getCategoryByID(int id);
        Task<Category> getNode(int categoryid);
        Task updateNode(int categoryid, string categoryname, string categorynameen, int? sortid, string href = null, string hrefen = null, bool ishref = false);
        Task<bool> delNode(int categoryid);

    }
}
