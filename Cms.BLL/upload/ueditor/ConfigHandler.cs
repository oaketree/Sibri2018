using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.upload.ueditor
{
    public class ConfigHandler : EditorHandler,IConfigHandler
    {
        private readonly IEditorConfig _editorConfig;
        public ConfigHandler(IHttpContextAccessor accessor, IEditorConfig editorConfig) : base(accessor)
        {
            _editorConfig = editorConfig;
        }
        public async Task Process()
        {
            await WriteJson(_editorConfig.Items);
        }
    }
}
