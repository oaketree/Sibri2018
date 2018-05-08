using Cms.BLL.category.services;
using Cms.BLL.category.viewmodels;
using Cms.BLL.news.viewmodels;
using Cms.BLL.upload.service;
using Cms.Contract.news;
using Core.DAL;
using Core.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cms.BLL.news.service
{
    public class NewsService : INewsService
    {
        private readonly NewsDBContext _dbContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUploadService _uploadService;
        private readonly IPictureHelper _pictureHelper;
        private readonly ICategoryService _categoryServices;

        protected readonly IHttpContextAccessor _accessor;

        public NewsService(NewsDBContext dbContext, IHostingEnvironment hostingEnvironment, IUploadService uploadService, IPictureHelper pictureHelper, ICategoryService categoryServices, IHttpContextAccessor accessor)
        {
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
            _uploadService = uploadService;
            _pictureHelper = pictureHelper;
            _categoryServices = categoryServices;
            _accessor = accessor;
        }

        public async Task<PaginatedList<News>> GetNewsList(int pageSize = 10, int pageIndex = 1, string keywords = "", string category = "")
        {
            Expression<Func<News, bool>> express = PredicateExtensions.True<News>();
            if (keywords != "")
            {
                express = express.And(n => n.Title.Contains(keywords) || n.NewsDetail.Contains(keywords));
            }
            if (category != "")
            {
                express = express.And(n => n.ColumnID == int.Parse(category));
            }
            return await PaginatedList<News>.CreateAsync(_dbContext.News.AsNoTracking().Where(express).OrderByDescending(o => o.RegDate), pageIndex, pageSize);
        }

        public async Task DelNews(int id)
        {
            var news = await _dbContext.News.AsNoTracking().SingleOrDefaultAsync(m => m.NewsID == id);
            if (news != null)
            {
                _dbContext.News.Remove(news);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<NewsView> GetNewsByID(int id)
        {
            var news = await _dbContext.News.AsNoTracking().FirstOrDefaultAsync(m => m.NewsID == id);
            if (news != null)
            {
                return new NewsView
                {
                    NewsID = news.NewsID,
                    Title = news.Title,
                    SubTitle = news.SubTitle,
                    Column = news.ColumnID.ToString(),
                    Language = (Language)news.Language,
                    Content = news.NewsDetail,
                    //IsPictureNews=news.IsPictureNews,
                    ImageUrl = news.NewsImageName
                };
            }
            else
                return null;

        }

        public async Task DelNewsImg(int id)
        {
            var news = await _dbContext.News.FirstOrDefaultAsync(n => n.NewsID == id);
            if (news != null)
            {
                if (news.NewsImageName != null && news.NewsImageName != string.Empty)
                {
                    File.Delete(Path.Combine($"{_hostingEnvironment.WebRootPath}/upload/image/", news.NewsImageName));
                    news.NewsImageName = string.Empty;
                    _dbContext.Update(news);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
        public async Task UpdateNews(int newsid, int column, int language, string title, string subTitle, string content, bool picnews)
        {
            var news = await _dbContext.News.AsNoTracking().FirstOrDefaultAsync(n => n.NewsID == newsid);
            if (news != null)
            {
                var fileName = string.Empty;
                var file = _accessor.HttpContext.Request.Form.Files["file"];
                if (file != null)
                {
                    var fileExtension = Path.GetExtension(file.FileName);
                    fileName = $"{DateTime.Now.ToString("yyMMddHHmmssfff")}{fileExtension}";
                    string webRootPath = _hostingEnvironment.WebRootPath;//获取物理地址
                    string path = $"upload/image/{fileName}";
                    var memoryStream = new MemoryStream();
                    await file.CopyToAsync(memoryStream);
                    if (picnews)
                    {
                        _pictureHelper.ProcessByStream(memoryStream, new PictureSize
                        {
                            Width = 200,
                            Height = 255,
                            Mode = "Cut"
                        });
                        memoryStream.Dispose();
                        memoryStream = _pictureHelper.Ms;
                    }
                    byte[] bytes = new byte[memoryStream.Length];
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    memoryStream.Read(bytes, 0, bytes.Length);
                    memoryStream.Dispose();
                    await File.WriteAllBytesAsync(Path.Combine(webRootPath, path), bytes);
                    await _uploadService.SaveToRemotePath(bytes, path);//保存到远程路径

                    await _uploadService.addFile(fileName, fileExtension, "News");
                    news.NewsImageName = fileName;

                }
                news.ColumnID = column;
                news.Language = language;
                news.Title = title;
                news.SubTitle = subTitle;
                news.NewsDetail = content;
                _dbContext.Update(news);
                await _dbContext.SaveChangesAsync();

            }
        }


        public async Task AddNews(NewsView nv, string[] checkbox)
        {
            if (nv.NewsImg != null)
            {
                var fileExtension = Path.GetExtension(nv.NewsImg.FileName);
                var fileName = $"{DateTime.Now.ToString("yyMMddHHmmssfff")}{fileExtension}";
                //var imgPath = Path.Combine("pic", fileName);
                string webRootPath =_hostingEnvironment.WebRootPath;//获取物理地址
                //string contentRootPath = _hostingEnvironment.ContentRootPath;
                string path = $"upload/image/{fileName}";
                var memoryStream = new MemoryStream();
                await nv.NewsImg.CopyToAsync(memoryStream);
                if (nv.IsPictureNews)
                {
                    _pictureHelper.ProcessByStream(memoryStream, new PictureSize
                    {
                        Width = 200,
                        Height = 255,
                        Mode = "Cut"
                    });
                    memoryStream.Dispose();
                    memoryStream = _pictureHelper.Ms;
                }
                byte[] bytes = new byte[memoryStream.Length];
                // 设置当前流的位置为流的开始
                memoryStream.Seek(0, SeekOrigin.Begin);
                memoryStream.Read(bytes, 0, bytes.Length);
                memoryStream.Dispose();
                //用下面的就不需要先seek
                //nv.NewsImg.OpenReadStream().Read(bytes, 0, bytes.Length);
                await File.WriteAllBytesAsync(Path.Combine(webRootPath, path), bytes);
                await _uploadService.SaveToRemotePath(bytes, path);//保存到远程路径
                

                foreach (var item in checkbox)
                {
                    _dbContext.News.Add(new News
                    {
                        ColumnID = int.Parse(item),
                        Language = (int)nv.Language,
                        Title = nv.Title,
                        SubTitle = nv.SubTitle,
                        NewsDetail = nv.Content,
                        //IsPictureNews = nv.IsPictureNews,
                        NewsImageName = fileName,

                    });
                    await _dbContext.SaveChangesAsync();
                }
                await _uploadService.addFile(fileName, fileExtension, "News");
            }
            else
            {
                foreach (var item in checkbox)
                {
                    _dbContext.News.Add(new News
                    {
                        ColumnID = int.Parse(item),
                        Language = (int)nv.Language,
                        Title = nv.Title,
                        SubTitle = nv.SubTitle,
                        NewsDetail = nv.Content,
                    });
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
