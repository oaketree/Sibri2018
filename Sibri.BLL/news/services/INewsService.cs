using Core.DAL;
using Sibri.Contract.pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sibri.BLL.news.services
{
    public interface INewsService
    {
        Task<List<News>> GetNewsList(int categoryid, int count, int language);
        Task<List<News>> GetPicNewsList(int categoryid, int count,int language);
        Task<PaginatedList<News>> GetNewsList(int categoryid, int language, int pageSize, int pageIndex);
        Task<PaginatedList<News>> GetNewsSearchList(string keywords, int language, int pageSize, int pageIndex);
        Task<News> GetNews(int newsid);
    }
}
