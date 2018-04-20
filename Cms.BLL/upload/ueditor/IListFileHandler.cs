using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.upload.ueditor
{
    public interface IListFileHandler
    {
        Task Process(string pathToList, string[] searchExtensions);
    }
}
