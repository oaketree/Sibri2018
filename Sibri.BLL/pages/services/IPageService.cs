using Sibri.Contract.pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sibri.BLL.pages.services
{
    public interface IPageService
    {
        Task<Pages> GetPage(int id,int language);
    }
}
