using Core.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sibri.Contract.pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sibri.BLL.news.services
{
    public class NewsService:INewsService
    {
        private readonly NewsDBContext _dbContext;

        public NewsService(NewsDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        [ResponseCache(Duration = 120, VaryByQueryKeys = new string[] { "categoryid", "count", "language" })]
        public async Task<List<News>> GetNewsList(int categoryid, int count,int language)
        {
            var list = await _dbContext.News.AsNoTracking().Where(n => n.ColumnID == categoryid).OrderByDescending(o => o.RegDate).Take(count).ToListAsync();
            if (list != null)
            {
                return list;
            }
            else {
                return null;
            }
        }
        [ResponseCache(Duration = 120, VaryByQueryKeys = new string[] { "categoryid", "count", "language" })]
        public async Task<List<News>> GetPicNewsList(int categoryid, int count,int language)
        {
            var list = await _dbContext.News.AsNoTracking().Where(n => n.ColumnID == categoryid&&!string.IsNullOrEmpty(n.NewsImageName)&&n.Language==language).OrderByDescending(o => o.RegDate).Take(count).ToListAsync();
            if (list != null)
            {
                return list;
            }
            else
            {
                return null;
            }
        }
        [ResponseCache(Duration = 120, VaryByQueryKeys = new string[] { "count", "language" })]
        public async Task<List<News>> GetPicNewsList(int count, int language)
        {
            var list = await _dbContext.News.AsNoTracking().Where(n => !string.IsNullOrEmpty(n.NewsImageName) && n.Language == language).OrderByDescending(o => o.RegDate).Take(count).ToListAsync();
            if (list != null)
            {
                return list;
            }
            else
            {
                return null;
            }
        }

        [ResponseCache(Duration = 120, VaryByQueryKeys = new string[] { "categoryid","language", "pageSize", "pageIndex" })]
        public Task<PaginatedList<News>> GetNewsList(int categoryid, int language, int pageSize, int pageIndex)
        {
            return PaginatedList<News>.CreateAsync(_dbContext.News.AsNoTracking().Where(n=>n.ColumnID==categoryid&&n.Language==language).OrderByDescending(o => o.RegDate), pageIndex, pageSize);
        }
        [ResponseCache(Duration = 120, VaryByQueryKeys = new string[] { "count", "language"})]
        public async Task<List<News>> GetNewsList(int count, int language)
        {
            var list = await _dbContext.News.AsNoTracking().Where(n => n.Language == language).OrderByDescending(o => o.RegDate).Take(count).ToListAsync();
            if (list != null)
            {
                return list;
            }
            else
            {
                return null;
            }
        }

        public Task<PaginatedList<News>> GetNewsSearchList(string keywords, int language, int pageSize, int pageIndex)
        {
            var key = Regex.Unescape(keywords);
            return PaginatedList<News>.CreateAsync(_dbContext.News.AsNoTracking().Where(n => n.Title.Contains(key) ||n.NewsDetail.Contains(key) && n.Language == language).OrderByDescending(o => o.RegDate), pageIndex, pageSize);
        }

        public async Task<News> GetNews(int newsid) {
            var news = await _dbContext.News.AsNoTracking().FirstOrDefaultAsync(n=>n.NewsID==newsid);
            if (news != null)
            {
                news.Hit = news.Hit + 1;
                _dbContext.Update(news);
                await _dbContext.SaveChangesAsync();
                return news;
            }
            else
                return null;

        }

    }
}
