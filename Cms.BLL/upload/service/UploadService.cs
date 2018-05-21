using Cms.Contract.upload;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.upload.service
{
    public class UploadService:IUploadService
    {
        private readonly UploadDBContext _dbContext;
        private RemoteSave _remoteSave;
        public UploadService(UploadDBContext dbContext, IOptions<RemoteSave> setting)
        {
            this._dbContext = dbContext;
            this._remoteSave = setting.Value;
        }
        public async Task addFile(string filename, string type, string position)
        {
            _dbContext.Uploads.Add(new Uploads
            {
                FileName = filename,
                Type = type,
                Position = position
            });
            await _dbContext.SaveChangesAsync();
        }
        //上传到其他文件夹的路径
        public async Task SaveToRemotePath(byte[] bytes,string path)
        {
            string basePath =this._remoteSave.BasePath;
            if (basePath != string.Empty) {
                var localPath = Path.Combine(basePath, path);
                if (!Directory.Exists(Path.GetDirectoryName(localPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(localPath));
                }
                await File.WriteAllBytesAsync(localPath, bytes);
            }
        }

    }
}
