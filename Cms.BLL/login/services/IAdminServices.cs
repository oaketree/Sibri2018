using Cms.Contract.login;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.login.services
{
    public interface IAdminServices
    {
        Task<Admins> getUser(string username,string password);
    }
}
