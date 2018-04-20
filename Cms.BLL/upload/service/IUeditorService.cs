using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.upload.service
{
    public interface IUeditorService
    {
        //Task addFile(string filename, string type, string position);
        Task configHandler();
        Task notSupportedHandler(string msg);
        Task uploadImageHandler();
        Task uploadScrawlHandler();
        Task uploadVideoHandler();
        Task uploadFileHandler();
        Task ListImageHandler();
        Task ListFileHandler();
        Task CatchImageHandler();

    }
}
