using Cms.Contract.login;
using Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.login.services
{
    public class AdminServices: IAdminServices
    {
        private readonly LoginDBContext _dbContext;
        public AdminServices(LoginDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Admins> getUser(Admins admin)
        {
            return await Task.Run(() => {
                return _dbContext.Admins.SingleOrDefault(n => n.UserName == admin.UserName && n.Password == admin.Password);
            });
        }
        //private LoginDBContext context;

    }
}
