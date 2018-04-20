using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.BLL.upload.service;
using Cms.BLL.upload.ueditor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cms.WebPage.Controllers
{
    public class UploadController : Controller
    {
        private readonly IUeditorService _ueditorService;

        public UploadController(IUeditorService ueditorService) {
            this._ueditorService = ueditorService;
        }

        [Authorize]
        public async Task Ueditor(string editorAction)
        {
            switch (editorAction)
            {
                case "config":
                    await _ueditorService.configHandler();
                    break;
                case "uploadimage":
                    await _ueditorService.uploadImageHandler();
                    break;
                case "uploadscrawl":
                    await _ueditorService.uploadScrawlHandler();
                    break;
                case "uploadvideo":
                    await _ueditorService.uploadVideoHandler();
                    break;
                case "uploadfile":
                    await _ueditorService.uploadFileHandler();
                    break;
                case "listimage":
                    await _ueditorService.ListImageHandler();
                    break;
                case "listfile":
                    await _ueditorService.ListFileHandler();
                    break;
                case "catchimage":
                    await _ueditorService.CatchImageHandler();
                    break;
                default:
                    await _ueditorService.notSupportedHandler(editorAction);
                    break;
            }

        }
    }
}