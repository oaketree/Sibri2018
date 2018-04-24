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
        public async Task addNodes(int? parentid, string categoryname, string categorynameen, int? sortid, string href = null, string hrefen = null,bool ishref=false)
        {
            _dbContext.Categorys.Add(new Category
            {
                ParentID = parentid,
                CategoryName = categoryname,
                CategoryNameEN = categorynameen,
                SortID = sortid,
                Href = href,
                HrefEn = hrefen,
                IsHref=ishref
                
            });
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<CategoryView>> categoryList()
        {
            var categorys = _dbContext.Categorys.AsNoTracking().Select(s => new CategoryView
            {
                text = $"{s.CategoryName}|{ s.CategoryNameEN}",
                categoryid = s.CategoryID,
                parentid = s.ParentID,
                sortid = s.SortID
            });
            var list= await categorys.ToListAsync();
            return HandleMenu<CategoryView>.SubMenu(list, 0);
        }

        public async Task<List<Category>> getCategoryByID(int id)
        {
            var categorys = await _dbContext.Categorys.AsNoTracking().Where(n => n.ParentID == id).ToListAsync();
            return categorys;
        }

        public IEnumerable<SelectListItem> getSelectListItemByID(int id)
        {
            var categorys = _dbContext.Categorys.AsNoTracking().Where(n => n.ParentID == id).Select(s => new SelectListItem() {
                Value=s.SortID.ToString(),
                Text=s.CategoryName
            }).AsEnumerable();
            return categorys;
        }

        public async Task<Category> getNode(int categoryid)
        {
            var category = await _dbContext.Categorys.FirstOrDefaultAsync(n => n.CategoryID == categoryid);
            return category;
        }

        public async Task updateNode(int categoryid, string categoryname, string categorynameen, int? sortid, string href = null, string hrefen = null, bool ishref = false)
        {
            var category = await _dbContext.Categorys.FirstOrDefaultAsync(n => n.CategoryID == categoryid);
            category.CategoryName = categoryname;
            category.CategoryNameEN = categorynameen;
            category.SortID = sortid;
            category.Href = href;
            category.HrefEn = hrefen;
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
