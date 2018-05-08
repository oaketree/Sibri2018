using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.upload.ueditor
{
    public class CrawlerHandler:EditorHandler,ICrawlerHandler
    {
        private ICrawler _crawler;

        private string[] Sources;
        //private List<Crawler> Crawlers;

        public CrawlerHandler(IHttpContextAccessor accessor, ICrawler crawler) : base(accessor)
        {
            this._crawler = crawler;
            //this.Crawlers = new List<Crawler>();
        }
        public async Task Process()
        {
            Sources = _accessor.HttpContext.Request.Form["source[]"];

            if (Sources == null || Sources.Length == 0)
            {
                await WriteJson(new
                {
                    state = "参数错误：没有指定抓取源"
                });
                return;
            }
            //Crawlers = Sources.Select( x => _crawler.Fetch(x)).ToArray();
            //var temp = Sources.Select(async x =>
            // {
            //     return await _crawler.Fetch(x);
            // });
            //foreach (var item in temp)
            //{
            //    this.Crawlers.Add(await item);
            //}
            await WriteJson(new
            {
                state = "SUCCESS",
                //list = temp.Select(async x => {
                //    var item = await x;
                //    return new
                //    {
                //        state = item.State,
                //        source = item.SourceUrl,
                //        url = item.ServerUrl
                //    };
                //}),
                list = Sources.Select(async x => {
                    var item = await _crawler.Fetch(x);
                    return new
                    {
                        state = item.State,
                        source = item.SourceUrl,
                        url = item.ServerUrl
                    };
                }),
            });
        }


    }
    
}
