using Cms.BLL.category.viewmodels;
using Cms.BLL.pages.viewmodels;
using Cms.BLL.upload.service;
using Cms.Contract.pages;
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

namespace Cms.BLL.pages.service
{
    public class PageService:IPageService
    {
        private readonly PageDBContext _dbContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IPictureHelper _pictureHelper;
        private readonly IUploadService _uploadService;
        protected readonly IHttpContextAccessor _accessor;
        public PageService(PageDBContext dbContext, IHostingEnvironment hostingEnvironment, IPictureHelper pictureHelper, IUploadService uploadService, IHttpContextAccessor accessor)
        {
            this._dbContext = dbContext;
            this._hostingEnvironment = hostingEnvironment;
            this._pictureHelper = pictureHelper;
            this._uploadService = uploadService;
            this._accessor = accessor;
        }
        public async Task DelPage(int id)
        {
            var news = await _dbContext.Pages.AsNoTracking().SingleOrDefaultAsync(m => m.PageID == id);
            if (news != null)
            {
                _dbContext.Pages.Remove(news);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<PaginatedList<Pages>> GetPageList(int pageSize = 10, int pageIndex = 1, string keywords = "")
        {
            Expression<Func<Pages, bool>> express = PredicateExtensions.True<Pages>();
            if (keywords != "")
            {
                express = express.And(n => n.Title.Contains(keywords)||n.PContent.Contains(keywords));
            }
            return await PaginatedList<Pages>.CreateAsync(_dbContext.Pages.AsNoTracking().Where(express).OrderByDescending(o => o.RegDate), pageIndex, pageSize);
        }

        public async Task AddPages(PageView pv)
        {
            if (pv.PageImg != null)
            {
                var fileExtension = Path.GetExtension(pv.PageImg.FileName);
                var fileName = $"{DateTime.Now.ToString("yyMMddHHmmssfff")}{fileExtension}";
                //var imgPath = Path.Combine("pic", fileName);
                string webRootPath = $"{_hostingEnvironment.WebRootPath}/upload/image/";//获取物理地址
                //string contentRootPath = _hostingEnvironment.ContentRootPath;
                if (pv.IsPicturePage)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await pv.PageImg.CopyToAsync(memoryStream);
                        _pictureHelper.ProcessByStream(memoryStream, Path.Combine(webRootPath, fileName), new Size
                        {
                            Width = 300,
                            Height = 255,
                            Mode = "Cut"
                        });
                    }
                }
                else
                {
                    using (var stream = new FileStream(Path.Combine(webRootPath, fileName), FileMode.CreateNew))
                    {
                        await pv.PageImg.CopyToAsync(stream);
                    }
                }
                _dbContext.Pages.Add(new Pages
                {
                    ColumnID = int.Parse(pv.Column),
                    Language = (int)pv.Language,
                    Title = pv.Title,
                    PContent = pv.PContent,
                    PageImageName = fileName,
                });
                await _dbContext.SaveChangesAsync();
                await _uploadService.addFile(fileName, fileExtension, "Page");
            }
            else
            {
                _dbContext.Pages.Add(new Pages
                {
                    ColumnID = int.Parse(pv.Column),
                    Language = (int)pv.Language,
                    Title = pv.Title,
                    PContent = pv.PContent,
                });
                await _dbContext.SaveChangesAsync();

            }
        }

        public async Task<PageView> GetPageByID(int id)
        {
            var pages = await _dbContext.Pages.AsNoTracking().FirstOrDefaultAsync(m => m.PageID == id);
            if (pages != null)
            {
                return new PageView
                {
                    PageID = pages.PageID,
                    Title = pages.Title,
                    Column = pages.ColumnID.ToString(),
                    Language = (Language)pages.Language,
                    PContent = pages.PContent,
                    ImageUrl = pages.PageImageName
                };
            }
            else
                return null;

        }
        public async Task DelPageImg(int id)
        {
            var pages = await _dbContext.Pages.FirstOrDefaultAsync(n => n.PageID == id);
            if (pages != null)
            {
                if (pages.PageImageName != null && pages.PageImageName != string.Empty)
                {
                    File.Delete(Path.Combine($"{_hostingEnvironment.WebRootPath}/upload/image/", pages.PageImageName));
                    pages.PageImageName = string.Empty;
                    _dbContext.Update(pages);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task UpdatePage(int pageid, int column, int language, string title, string content, bool picpage)
        {
            
            var page = await _dbContext.Pages.AsNoTracking().FirstOrDefaultAsync(n => n.PageID == pageid);
            if (page != null)
            {
                var fileName = string.Empty;
                var file = _accessor.HttpContext.Request.Form.Files["file"];
                if (file != null)
                {
                    var fileExtension = Path.GetExtension(file.FileName);
                    fileName = $"{DateTime.Now.ToString("yyMMddHHmmssfff")}{fileExtension}";
                    string webRootPath = $"{_hostingEnvironment.WebRootPath}/upload/image/";//获取物理地址

                    if (picpage)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);
                            _pictureHelper.ProcessByStream(memoryStream, Path.Combine(webRootPath, fileName), new Size
                            {
                                Width = 200,
                                Height = 255,
                                Mode = "Cut"
                            });

                        }
                    }
                    else
                    {
                        using (var stream = new FileStream(Path.Combine(webRootPath, fileName), FileMode.CreateNew))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                    await _uploadService.addFile(fileName, fileExtension, "Page");
                    page.PageImageName = fileName;

                }
                page.ColumnID = column;
                page.Language = language;
                page.Title = title;
                page.PContent = content;
                _dbContext.Update(page);
                await _dbContext.SaveChangesAsync();
            }
            

        }


    }
}
