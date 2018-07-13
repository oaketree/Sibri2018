using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sibri.Contract.pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sibri.BLL.pages.services
{
    public class PageService : IPageService
    {
        private readonly PageDBContext _dbContext;
        public PageService(PageDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        [ResponseCache(Duration = 120, VaryByQueryKeys = new string[] { "id", "language" })]
        public async Task<Pages> GetPage(int id, int language)
        {
            var page = await _dbContext.Pages.AsNoTracking().FirstOrDefaultAsync(p => p.ColumnID == id && p.Language == language);
            if (page != null)
            {
                page.Hit = page.Hit + 1;
                _dbContext.Update(page);
                await _dbContext.SaveChangesAsync();
                return page;
            }
            else
                return null;
        }
    }
}
