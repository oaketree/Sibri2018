using Cms.BLL.upload.service;
using Core.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.BLL.upload.ueditor
{
    public class UploadHandler:EditorHandler,IUploadHandler
    {
        private UploadResult _result;
        private UploadConfig _uploadConfig;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IPictureHelper _pictureHelper;

        private readonly IUploadService _uploadService;
        public UploadHandler(IHttpContextAccessor accessor, IHostingEnvironment hostingEnvironment, IUploadService uploadService, IPictureHelper pictureHelper) : base(accessor)
        {
            this._hostingEnvironment = hostingEnvironment;
            this._uploadService = uploadService;
            this._pictureHelper = pictureHelper;
            this._result= new UploadResult() { State = UploadState.Unknown };
        }
        
        public async Task Process(UploadConfig uploadConfig)
        {
            this._uploadConfig = uploadConfig;
            byte[] uploadFileBytes = null;
            string uploadFileName = null;

            if (_uploadConfig.Base64)
            {
                uploadFileName = _uploadConfig.Base64Filename;
                uploadFileBytes = Convert.FromBase64String(_accessor.HttpContext.Request.Query[_uploadConfig.UploadFieldName]);//注意可能会错
            }
            else {
                var file = _accessor.HttpContext.Request.Form.Files[_uploadConfig.UploadFieldName];
                uploadFileName = file.FileName;

                if (!CheckFileType(uploadFileName))
                {
                    this._result.State = UploadState.TypeNotAllow;
                    await WriteResult();
                    return;
                }
                if (!CheckFileSize(file.Length))
                {
                    this._result.State = UploadState.SizeLimitExceed;
                    await WriteResult();
                    return;
                }
                //uploadFileBytes = new byte[file.Length];
                try
                {
                    var memoryStream = new MemoryStream();
                    await file.CopyToAsync(memoryStream);
                    if (_uploadConfig.ActionName == "uploadimage")
                    {
                        _pictureHelper.ProcessByStream(memoryStream, new PictureSize
                        {
                            Width = 700,
                            Height=700,
                            Mode = "Auto"
                        });
                        memoryStream.Dispose();
                        memoryStream = _pictureHelper.Ms;
                    }
                    uploadFileBytes = new byte[memoryStream.Length];
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    memoryStream.Read(uploadFileBytes, 0, uploadFileBytes.Length);
                    memoryStream.Dispose();
                    //file.InputStream.Read(uploadFileBytes, 0, file.Length);
                    //file.OpenReadStream().Read(uploadFileBytes, 0, uploadFileBytes.Length);

                }
                catch (Exception)
                {
                    _result.State = UploadState.NetworkError;
                    await WriteResult();
                }
            }
            _result.OriginFileName = uploadFileName;

            var savePath = PathFormatter.Format(uploadFileName, _uploadConfig.PathFormat);

            string extension = Path.GetExtension(uploadFileName);
            string filename = Path.GetFileName(savePath);

            var localPath = Path.Combine(_hostingEnvironment.WebRootPath, savePath);
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(localPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(localPath));
                }
                
                //File.WriteAllBytes(localPath, uploadFileBytes);
                await File.WriteAllBytesAsync(localPath, uploadFileBytes);
                await _uploadService.SaveToRemotePath(uploadFileBytes, savePath);//保存到远程路径

                await _uploadService.addFile(filename,extension,"Ueditor");
                _result.Url = savePath;
                _result.State = UploadState.Success;
            }
            catch (Exception e)
            {
                _result.State = UploadState.FileAccessError;
                _result.ErrorMessage = e.Message;
            }
            finally
            {
                await WriteResult();
            }


        }

        private async Task WriteResult()
        {
            await WriteJson(new
            {
                state = GetStateMessage(_result.State),
                url = _result.Url,
                title = _result.OriginFileName,
                original = _result.OriginFileName,
                error = _result.ErrorMessage
            });
        }

        private string GetStateMessage(UploadState state)
        {
            switch (state)
            {
                case UploadState.Success:
                    return "SUCCESS";
                case UploadState.FileAccessError:
                    return "文件访问出错，请检查写入权限";
                case UploadState.SizeLimitExceed:
                    return "文件大小超出服务器限制";
                case UploadState.TypeNotAllow:
                    return "不允许的文件格式";
                case UploadState.NetworkError:
                    return "网络错误";
            }
            return "未知错误";
        }

        private bool CheckFileType(string filename)
        {
            var fileExtension = Path.GetExtension(filename).ToLower();
            return _uploadConfig.AllowExtensions.Select(x => x.ToLower()).Contains(fileExtension);
        }

        private bool CheckFileSize(long size)
        {
            return size < _uploadConfig.SizeLimit;
        }
    }

    public class UploadResult
    {
        public UploadState State { get; set; }
        public string Url { get; set; }
        public string OriginFileName { get; set; }

        public string ErrorMessage { get; set; }
    }

    public enum UploadState
    {
        Success = 0,
        SizeLimitExceed = -1,
        TypeNotAllow = -2,
        FileAccessError = -3,
        NetworkError = -4,
        Unknown = 1,
    }
}
