using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.BLL.upload.ueditor
{
    public interface IEditorConfig
    {
        JObject Items { get;}
        string[] GetStringList(string key);
        string GetString(string key);
        int GetInt(string key);

    }
}
