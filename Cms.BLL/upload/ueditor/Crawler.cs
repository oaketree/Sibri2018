using Cms.BLL.upload.service;
using Core.Utility;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.upload.ueditor
{
    public class Crawler:ICrawler
    {
        private string sourceUrl;
        private string serverUrl;
        private string state;

        private readonly IEditorConfig _editorConfig;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUploadService _uploadService;
        private readonly IPictureHelper _pictureHelper;
        public Crawler(IEditorConfig editorConfig, IHostingEnvironment hostingEnvironment, IUploadService uploadService, IPictureHelper pictureHelper) {
            this._editorConfig = editorConfig;
            this._hostingEnvironment = hostingEnvironment;
            this._uploadService = uploadService;
            this._pictureHelper = pictureHelper;
        }

        public string SourceUrl
        {
            get
            {
                return this.sourceUrl;
            }
        }
        public string ServerUrl
        {
            get
            {
                return this.serverUrl;
            }
        }
        public string State
        {
            get
            {
                return this.state;
            }
        }

        public async Task<Crawler> Fetch(string sourceUrl)
        {
            this.sourceUrl = sourceUrl;
            if (!IsExternalIPAddress(this.sourceUrl))
            {
                state = "INVALID_URL";
                return this;
            }
            var request = WebRequest.Create(this.sourceUrl) as WebRequest;
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    state = "Url returns " + response.StatusCode + ", " + response.StatusDescription;
                    return this;
                }
                if (response.ContentType.IndexOf("image") == -1)
                {
                    state = "Url is not an image";
                    return this;
                }
                serverUrl = PathFormatter.Format(Path.GetFileName(this.sourceUrl), _editorConfig.GetString("catcherPathFormat"));
                //var savePath = Server.MapPath(ServerUrl);
                var savePath =Path.Combine(_hostingEnvironment.WebRootPath,serverUrl);

                string extension = Path.GetExtension(serverUrl);
                string filename = Path.GetFileName(serverUrl);

                if (!Directory.Exists(Path.GetDirectoryName(savePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                }
                try
                {
                    WebClient webClient = new WebClient();
                    webClient.Credentials = CredentialCache.DefaultCredentials;
                    byte[] bytes = await webClient.DownloadDataTaskAsync(this.sourceUrl);
                    //byte[] bytes = webClient.DownloadData(this.sourceUrl);
                    webClient.Dispose();
                    //var memoryStream = response.GetResponseStream();
                    //_pictureHelper.ProcessByStream(memoryStream, new PictureSize
                    //{
                    //    Width = 700,
                    //    Mode = "W"
                    //});
                    //memoryStream.Dispose();
                    //memoryStream = _pictureHelper.Ms;

                    //byte[] bytes = new byte[memoryStream.Length];
                    //memoryStream.Seek(0, SeekOrigin.Begin);
                    //memoryStream.Read(bytes, 0, bytes.Length);


                    //memoryStream.Dispose();需要的

                    //var reader = new BinaryReader(memoryStream);
                    //byte[] bytes;
                    //using (var ms = new MemoryStream())
                    //{
                    //    byte[] buffer = new byte[4096];
                    //    int count;
                    //    while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
                    //    {
                    //        ms.Write(buffer, 0, count);
                    //    }
                    //    bytes = ms.ToArray();
                    //}
                    //File.WriteAllBytes(savePath, bytes);
                    await File.WriteAllBytesAsync(savePath, bytes);
                    await _uploadService.SaveToRemotePath(bytes, serverUrl);//保存到远程路径
                    await _uploadService.addFile(filename, extension, "Ueditor");
                    state = "SUCCESS";
                }
                catch (Exception e)
                {
                    state = "抓取错误：" + e.Message;
                }
                return this;
            }
        }
        private bool IsExternalIPAddress(string url)
        {
            var uri = new Uri(url);
            switch (uri.HostNameType)
            {
                case UriHostNameType.Dns:
                    var ipHostEntry = Dns.GetHostEntry(uri.DnsSafeHost);
                    foreach (IPAddress ipAddress in ipHostEntry.AddressList)
                    {
                        byte[] ipBytes = ipAddress.GetAddressBytes();
                        if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            if (!IsPrivateIP(ipAddress))
                            {
                                return true;
                            }
                        }
                    }
                    break;

                case UriHostNameType.IPv4:
                    return !IsPrivateIP(IPAddress.Parse(uri.DnsSafeHost));
            }
            return false;
        }

        private bool IsPrivateIP(IPAddress myIPAddress)
        {
            if (IPAddress.IsLoopback(myIPAddress)) return true;
            if (myIPAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                byte[] ipBytes = myIPAddress.GetAddressBytes();
                // 10.0.0.0/24 
                if (ipBytes[0] == 10)
                {
                    return true;
                }
                // 172.16.0.0/16
                else if (ipBytes[0] == 172 && ipBytes[1] == 16)
                {
                    return true;
                }
                // 192.168.0.0/16
                else if (ipBytes[0] == 192 && ipBytes[1] == 168)
                {
                    return true;
                }
                // 169.254.0.0/16
                else if (ipBytes[0] == 169 && ipBytes[1] == 254)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
