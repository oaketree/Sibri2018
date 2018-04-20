using Cms.BLL.news.viewmodels;
using Cms.BLL.upload.service;
using Cms.Contract.news;
using Core.Utility;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Cms.BLL.news.service
{
    public class NewsService:INewsService
    {
        private readonly NewsDBContext _dbContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUploadService _uploadService;
        private readonly IPictureHelper _pictureHelper;

        public NewsService(NewsDBContext dbContext, IHostingEnvironment hostingEnvironment, IUploadService uploadService, IPictureHelper pictureHelper)
        {
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
            _uploadService = uploadService;
            _pictureHelper = pictureHelper;
        }

        public async Task addNews(NewsView nv,string[] checkbox)
        {
            if (nv.NewsImg!=null)
            {
                var fileExtension = Path.GetExtension(nv.NewsImg.FileName);
                var fileName = $"{DateTime.Now.ToString("yyMMddHHmmssfff")}{fileExtension}";
                //var imgPath = Path.Combine("pic", fileName);
                string webRootPath = $"{_hostingEnvironment.WebRootPath}/upload/image/";//获取物理地址
                //string contentRootPath = _hostingEnvironment.ContentRootPath;
                if (nv.IsPictureNews)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await nv.NewsImg.CopyToAsync(memoryStream);
                        _pictureHelper.ProcessByStream(memoryStream, Path.Combine(webRootPath, fileName), new Size
                        {
                            Width = 200,
                            Height = 255,
                            Mode = "Cut"
                        });
                    }
                }
                else {
                    using (var stream = new FileStream(Path.Combine(webRootPath, fileName), FileMode.CreateNew))
                    {
                        await nv.NewsImg.CopyToAsync(stream);
                    }
                }
                foreach (var item in checkbox)
                {
                    _dbContext.News.Add(new News
                    {
                        ColumnID = int.Parse(item),
                        Language = nv.Language,
                        Title = nv.Title,
                        SubTitle = nv.SubTitle,
                        NewsDetail = nv.Content,
                        IsPictureNews = nv.IsPictureNews,
                        NewsImageName = fileName,

                    });
                    await _dbContext.SaveChangesAsync();
                }
                await _uploadService.addFile(fileName, fileExtension,"News");
            }
            else
            {
                foreach (var item in checkbox)
                {
                    _dbContext.News.Add(new News
                    {
                        ColumnID = int.Parse(item),
                        Language = nv.Language,
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
