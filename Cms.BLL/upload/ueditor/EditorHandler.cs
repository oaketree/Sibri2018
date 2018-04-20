using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.upload.ueditor
{
    public abstract class EditorHandler
    {
        //private HttpContext _accessor;
        protected IHttpContextAccessor _accessor;

        public EditorHandler(IHttpContextAccessor accessor)
        {
            this._accessor = accessor;
        }

        //public abstract Task Process(string msg = "");

        protected async Task WriteJson(object response)
        {
            string jsonpCallback = _accessor.HttpContext.Request.Query["callback"];
            string json = JsonConvert.SerializeObject(response);
            if (String.IsNullOrWhiteSpace(jsonpCallback))
            {
                _accessor.HttpContext.Response.Headers.Add("Content-Type", "text/plain");
                await _accessor.HttpContext.Response.WriteAsync(json);
            }
            else
            {
                _accessor.HttpContext.Response.Headers.Add("Content-Type", "application/javascript");
                await _accessor.HttpContext.Response.WriteAsync($"{jsonpCallback}({json});");
            }
            //_accessor.Response.Clear();
        }


        //public HttpRequest Request { get; private set; }
        //public HttpResponse Response { get; private set; }
        //public HttpContext Context { get; private set; }
        //public HttpServerUtility Server { get; private set; }
    }
}
