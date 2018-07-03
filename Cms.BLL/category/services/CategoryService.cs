using Cms.BLL.category.viewmodels;
using Cms.Contract.category;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.BLL.category.services
{
    public class CategoryService: ICategoryService
    {
        private readonly CategoryDBContext _dbContext;
        public CategoryService(CategoryDBContext dbContext) {
            this._dbContext = dbContext;
        }
        public async Task addNodes(int? parentid, string categoryname, string categorynameen, int? sortid, string href,bool ishref)
        {
            _dbContext.Categorys.Add(new Category
            {
                ParentID = parentid,
                CategoryName = categoryname,
                CategoryNameEN = categorynameen,
                SortID = sortid,
                Href = href,
                //HrefEn = hrefen,
                IsHref=ishref
                
            });
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<CategoryView>> categoryList()
        {
            var categorys = _dbContext.Categorys.AsNoTracking().Select(s => new CategoryView
            {
                text = $"{s.CategoryName}|{ s.CategoryNameEN} ID:{s.CategoryID}",
                categoryid = s.CategoryID,
                parentid = s.ParentID,
                sortid = s.SortID
            });
            var list= await categorys.OrderBy(o=>o.parentid).ThenBy(t=>t.sortid).ToListAsync();
            return HandleMenu<CategoryView>.SubMenu(list, 0);
        }

        public async Task<List<Category>> getCategoryByID(int? id)
        {
            List<Category> categorys = null;
            if (id != null)
            {
                categorys = await _dbContext.Categorys.AsNoTracking().Where(n => n.ParentID == id).ToListAsync();
            }
            else
            {
                categorys = await _dbContext.Categorys.AsNoTracking().ToListAsync();
            }
            return categorys;
        }
        //public async Task<Category> getCategoryNameByID(int id)
        //{
        //    var categorys = await _dbContext.Categorys.AsNoTracking().FirstOrDefaultAsync(n=>n.CategoryID==id);
        //    return categorys;
        //}


        public Task<List<SelectListItem>> getSelectListItemByID(int? id = null)
        {
            IQueryable<SelectListItem> categorys = null;
            if (id != null){
                categorys = _dbContext.Categorys.AsNoTracking().Where(n => n.ParentID == id).Select(s => new SelectListItem()
                {
                    Value = s.CategoryID.ToString(),
                    Text = s.CategoryName
                });
            }
            else {
                categorys = _dbContext.Categorys.AsNoTracking().Select(s => new SelectListItem()
                {
                    Value = s.CategoryID.ToString(),
                    Text = s.CategoryName
                });
            }
            return categorys.ToListAsync();
        }

        public Task<Category> getNode(int categoryid)
        {
            var category = _dbContext.Categorys.FirstOrDefaultAsync(n => n.CategoryID == categoryid);
            return category;
        }

        public async Task updateNode(int categoryid, string categoryname, string categorynameen, int? sortid, string href, bool ishref)
        {
            var category = await _dbContext.Categorys.FirstOrDefaultAsync(n => n.CategoryID == categoryid);
            category.CategoryName = categoryname;
            category.CategoryNameEN = categorynameen;
            category.SortID = sortid;
            category.Href = href;
            //category.HrefEn = hrefen;
            category.IsHref = ishref;
            _dbContext.Update(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> delNode(int categoryid)
        {
            var childNode = _dbContext.Categorys.FirstOrDefault(n => n.ParentID == categoryid);
            if (childNode != null)
            {
                return false;
            }
            else {
                var node= _dbContext.Categorys.FirstOrDefault(n => n.CategoryID == categoryid);
                if (node != null) {
                    _dbContext.Categorys.Remove(node);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
        }


    }
}
