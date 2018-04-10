using Cms.Contract.login;
using Core.DAL;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Admins> getUser(string username, string password)
        {
            //return await _dbContext.Admins.FindAsync(username, password);
            //return await Task.Run(() => {
            //    return _dbContext.Admins.SingleOrDefault(n => n.UserName == username && n.Password == password);
            //});
            return await _dbContext.Admins.AsNoTracking().FirstOrDefaultAsync(n => n.UserName == username && n.Password == password);
        }

        //public async Task<bool> checkUserExist(string username)
        //{
        //    var user = await Task.Run(() =>
        //      {
        //          return _dbContext.Admins.SingleOrDefault(n => n.UserName == username);
        //      });
        //    if (user != null)
        //        return true;
        //    else
        //        return false;
        //}
        public async Task<bool> addUser(string username, string password)
        {
            var user = await _dbContext.Admins.AsNoTracking().FirstOrDefaultAsync(n => n.UserName == username);
            if (user != null)
            {
                return false;
            }
            else {
                _dbContext.Admins.Add(new Admins {

                    UserName = username,
                    Password = password
                });
                await _dbContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<Admins>> userList()
        {
            return await _dbContext.Admins.AsNoTracking().ToListAsync();
        }

    }
}
