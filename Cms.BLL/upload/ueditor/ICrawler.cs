using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.upload.ueditor
{
    public interface ICrawler
    {
        Task<Crawler> Fetch(string sourceUrl);
        //string SourceUrl { get; }
        //string ServerUrl { get; }
        //string State { get; }
    }
}
