using Cms.BLL.upload.ueditor;
using Cms.Contract.upload;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.upload.service
{
    public class UeditorService:IUeditorService
    {
        private readonly INotSupportedHandler _notSupportedHandler;
        private readonly IConfigHandler _configHandler;
        private readonly IUploadHandler _uploadHandler;
        private readonly IEditorConfig _editorConfig;
        private readonly IListFileHandler _listFileHandler;
        private readonly ICrawlerHandler _crawlerHandler;

        public UeditorService(UploadDBContext dbContext, INotSupportedHandler NotSupportedHandler, IConfigHandler configHandler, IUploadHandler uploadHandler,IEditorConfig editorConfig, IListFileHandler listFileHandler, ICrawlerHandler crawlerHandler) {
            
            this._notSupportedHandler = NotSupportedHandler;
            this._configHandler = configHandler;
            this._uploadHandler = uploadHandler;
            this._editorConfig = editorConfig;
            this._listFileHandler = listFileHandler;
            this._crawlerHandler = crawlerHandler;
        }


        

        public async Task configHandler()
        {
            await _configHandler.Process();
        }

        public async Task notSupportedHandler(string msg)
        {
            await _notSupportedHandler.Process(msg);
        }

        public async Task uploadImageHandler()
        {
            await _uploadHandler.Process(new UploadConfig()
            {
                AllowExtensions = _editorConfig.GetStringList("imageAllowFiles"),
                PathFormat = _editorConfig.GetString("imagePathFormat"),
                SizeLimit = _editorConfig.GetInt("imageMaxSize"),
                UploadFieldName = _editorConfig.GetString("imageFieldName"),
                ActionName= _editorConfig.GetString("imageActionName")
            });
        }

        public async Task uploadScrawlHandler()
        {
            await _uploadHandler.Process(new UploadConfig()
            {
                AllowExtensions = new string[] { ".png" },
                PathFormat = _editorConfig.GetString("scrawlPathFormat"),
                SizeLimit = _editorConfig.GetInt("scrawlMaxSize"),
                UploadFieldName = _editorConfig.GetString("scrawlFieldName"),
                Base64 = true,
                Base64Filename = "scrawl.png",
                ActionName = _editorConfig.GetString("scrawlActionName")
            });
        }

        public async Task uploadVideoHandler()
        {
            await _uploadHandler.Process(new UploadConfig()
            {
                AllowExtensions = _editorConfig.GetStringList("videoAllowFiles"),
                PathFormat = _editorConfig.GetString("videoPathFormat"),
                SizeLimit = _editorConfig.GetInt("videoMaxSize"),
                UploadFieldName = _editorConfig.GetString("videoFieldName"),
                ActionName = _editorConfig.GetString("videoActionName")
            });
        }
        public async Task uploadFileHandler()
        {
            await _uploadHandler.Process(new UploadConfig()
            {
                AllowExtensions = _editorConfig.GetStringList("fileAllowFiles"),
                PathFormat = _editorConfig.GetString("filePathFormat"),
                SizeLimit = _editorConfig.GetInt("fileMaxSize"),
                UploadFieldName = _editorConfig.GetString("fileFieldName"),
                ActionName = _editorConfig.GetString("fileActionName")
            });
        }

        public async Task ListImageHandler()
        {
            await _listFileHandler.Process(_editorConfig.GetString("imageManagerListPath"), _editorConfig.GetStringList("imageManagerAllowFiles"));
        }
        public async Task ListFileHandler()
        {
            await _listFileHandler.Process(_editorConfig.GetString("fileManagerListPath"), _editorConfig.GetStringList("fileManagerAllowFiles"));
        }
        public async Task CatchImageHandler()
        {
            await _crawlerHandler.Process();
        }




    }
}
