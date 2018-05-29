using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Sibri.BLL.category.viewmodels;
using Sibri.BLL.share;
using Sibri.Contract.category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sibri.BLL.category.services
{
    public class CategoryService: ICategoryService
    {
        private readonly CategoryDBContext _dbContext;
        private IMemoryCache _cache;


        public CategoryService(CategoryDBContext dbContext, IMemoryCache cache) {
            this._dbContext = dbContext;
            this._cache = cache;
        }

        private Task<List<Category>> GetCategoryCache()
        {
            var cacheEntry = _cache.GetOrCreate(CacheKeys.Entry, entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(5);
                return _dbContext.Categorys.AsNoTracking().ToListAsync();
            });
            return cacheEntry;
        }


        public async Task<List<Category>> GetCategoryListByID(int? id,int direction)
        {
            if (id != null)
            {
                var categoryList = await GetCategoryCache();
                if (direction == 0)
                {
                    var category = categoryList.FirstOrDefault(n => n.CategoryID == id);
                    if (category != null)
                    {
                        if (category.ParentID == 0)
                        {
                            var temp = new List<Category>();
                            temp.Add(category);
                            return temp;
                        }
                        return categoryList.Where(n => n.ParentID == category.ParentID).ToList();
                    }
                    else
                        return null;
                }
                else {
                    var category = categoryList.Where(n => n.ParentID == id);
                    if (category.Count()!= 0)
                    {
                        return category.ToList();
                    }
                    else
                    {
                        return await GetCategoryListByID(id, 0);
                    }

                }
               
            }
            else
                return null;
        }

        public async Task<List<CategoryView>> GetProductCategoryListCn(int topid)
        {
            var categoryList = await GetCategoryCache();
            var list = categoryList.Select(s => new CategoryView
            {
                text = s.CategoryName,
                categoryid = s.CategoryID,
                parentid = s.ParentID,
                sortid = s.SortID
            }).OrderBy(o => o.parentid).ThenBy(t => t.sortid).ToList();
            return HandleMenu.SubMenu(list, topid);

        }
        public async Task<List<CategoryView>> GetProductCategoryListEn(int topid)
        {
            var categoryList = await GetCategoryCache();
            var list = categoryList.Select(s => new CategoryView
            {
                text = s.CategoryNameEN,
                categoryid = s.CategoryID,
                parentid = s.ParentID,
                sortid = s.SortID
            }).OrderBy(o => o.parentid).ThenBy(t => t.sortid).ToList();
            return HandleMenu.SubMenu(list, topid);
        }


        public async Task<Category> GetParentByID(int id)
        {
            var categoryList = await GetCategoryCache();
            var category = categoryList.FirstOrDefault(n => n.CategoryID == id);
            if (category != null)
            {
                if (category.ParentID.Value == 0)
                    return category;
                return categoryList.FirstOrDefault(n => n.CategoryID == category.ParentID.Value);
            }
            else
                return null;
            
        }
        public async Task<Category> GetCategoryByID(int id)
        {
            var categoryList = await GetCategoryCache();
            var category = categoryList.FirstOrDefault(n => n.CategoryID == id);
            if (category != null)
                return category;
            else
                return null;

        }






    }
}
