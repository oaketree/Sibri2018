using Cms.BLL.news.viewmodels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.news.service
{
    public interface INewsService
    {
        Task addNews(NewsView nv, string[] checkbox);

    }
}
