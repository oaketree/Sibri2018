using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cms.BLL.upload.ueditor
{
    public class EditorConfig:IEditorConfig
    {
        private static bool noCache = true;
        private static JObject _Items;
        private readonly IHostingEnvironment _hostingEnvironment;

        public EditorConfig(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        private JObject BuildItems()
        {
            var hostingEnvironment = new HostingEnvironment();
            var root= $"{_hostingEnvironment.WebRootPath}/lib/ueditor/";
            var path = Path.Combine(root,"config.json");
            var json = File.ReadAllText(path);
            return JObject.Parse(json);
        }
        public JObject Items
        {
            get
            {
                if (noCache || _Items == null)
                {
                    _Items = BuildItems();
                }
                return _Items;
            }
          
        }

        public  T GetValue<T>(string key)
        {
            return Items[key].Value<T>();
        }

        public string[] GetStringList(string key)
        {
            return Items[key].Select(x => x.Value<String>()).ToArray();
        }

        public string GetString(string key)
        {
            return GetValue<String>(key);
        }

        public int GetInt(string key)
        {
            return GetValue<int>(key);
        }
    }
}
