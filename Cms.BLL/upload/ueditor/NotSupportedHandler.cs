using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.upload.ueditor
{
    public class NotSupportedHandler:EditorHandler,INotSupportedHandler
    {
        //private IHttpContextAccessor _accessor;
        public NotSupportedHandler(IHttpContextAccessor _accessor)
        : base(_accessor)
        {
        }

        public async Task Process(string msg="")
        {
            await WriteJson(new
            {
                state = msg
            });
        }
    }
}
