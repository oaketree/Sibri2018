using Cms.Contract.upload;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.upload.service
{
    public class UploadService:IUploadService
    {
        private readonly UploadDBContext _dbContext;
        public UploadService(UploadDBContext dbContext)
        {
            this._dbContext = dbContext;
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
    }
}
