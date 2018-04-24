using Cms.BLL.news.viewmodels;
using Cms.Contract.news;
using Core.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.news.service
{
    public interface INewsService
    {
        Task AddNews(NewsView nv, string[] checkbox);
        Task<PaginatedList<News>> GetNewsList(int pageSize, int pageIndex, string keywords = null, string category = null);

    }
}
