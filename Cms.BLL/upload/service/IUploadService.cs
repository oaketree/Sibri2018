using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.upload.service
{
    public interface IUploadService
    {
        Task addFile(string filename, string type, string position);
        Task SaveToRemotePath(byte[] bytes, string path);
    }
}
