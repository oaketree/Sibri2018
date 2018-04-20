using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.upload.ueditor
{
    public class ListFileHandler:EditorHandler,IListFileHandler
    {
        enum ResultState
        {
            Success,
            InvalidParam,
            AuthorizError,
            IOError,
            PathNotFound
        }

        private int Start;
        private int Size;
        private int Total;
        private ResultState State;
        private string PathToList;
        private string[] FileList;
        private string[] SearchExtensions;

        private readonly IEditorConfig _editorConfig;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ListFileHandler(IHttpContextAccessor accessor, IEditorConfig editorConfig, IHostingEnvironment hostingEnvironment) : base(accessor)
        {
            this._editorConfig = editorConfig;
            this._hostingEnvironment = hostingEnvironment;
        }

        public async Task Process(string pathToList, string[] searchExtensions)
        {
            this.SearchExtensions = searchExtensions.Select(x => x.ToLower()).ToArray();
            this.PathToList = pathToList;

            try
            {
                Start = String.IsNullOrEmpty(_accessor.HttpContext.Request.Query["start"]) ? 0 : Convert.ToInt32(_accessor.HttpContext.Request.Query["start"]);
                Size = String.IsNullOrEmpty(_accessor.HttpContext.Request.Query["size"]) ? _editorConfig.GetInt("imageManagerListSize") : Convert.ToInt32(_accessor.HttpContext.Request.Query["size"]);
            }
            catch (FormatException)
            {
                State = ResultState.InvalidParam;
                await WriteResult();
                return;
            }
            var buildingList = new List<String>();
            try
            {

                //var localPath = Server.MapPath(PathToList);
                var localPath = Path.Combine(_hostingEnvironment.WebRootPath, PathToList);

                buildingList.AddRange(Directory.GetFiles(localPath, "*", SearchOption.AllDirectories)
                    .Where(x => SearchExtensions.Contains(Path.GetExtension(x).ToLower()))
                    .Select(x => PathToList + x.Substring(localPath.Length).Replace("\\", "/")));
                Total = buildingList.Count;
                FileList = buildingList.OrderBy(x => x).Skip(Start).Take(Size).ToArray();
            }
            catch (UnauthorizedAccessException)
            {
                State = ResultState.AuthorizError;
            }
            catch (DirectoryNotFoundException)
            {
                State = ResultState.PathNotFound;
            }
            catch (IOException)
            {
                State = ResultState.IOError;
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
                state = GetStateString(),
                list = FileList == null ? null : FileList.Select(x => new { url = x }),
                start = Start,
                size = Size,
                total = Total
            });
        }
        private string GetStateString()
        {
            switch (State)
            {
                case ResultState.Success:
                    return "SUCCESS";
                case ResultState.InvalidParam:
                    return "参数不正确";
                case ResultState.PathNotFound:
                    return "路径不存在";
                case ResultState.AuthorizError:
                    return "文件系统权限不足";
                case ResultState.IOError:
                    return "文件系统读取错误";
            }
            return "未知错误";
        }



    }
}
