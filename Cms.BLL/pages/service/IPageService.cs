using Cms.BLL.pages.viewmodels;
using Cms.Contract.pages;
using Core.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.pages.service
{
    public interface IPageService
    {
        Task AddPages(PageView pv);
        Task<PaginatedList<Pages>> GetPageList(int pageSize = 10, int pageIndex = 1, string keywords = "");
        Task DelPage(int id);
        Task<PageView> GetPageByID(int id);
        Task DelPageImg(int id);
        Task UpdatePage(int pageid, int column, int language, string title, string content, bool picpage);
    }
}
