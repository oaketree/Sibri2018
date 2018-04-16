using Cms.Contract.login;
using Core.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public async Task<bool> addUser(string username, string password,string role)
        {
            var user = await _dbContext.Admins.AsNoTracking().FirstOrDefaultAsync(n => n.UserName == username);
            if (user != null)
            {
                return false;
            }
            else {
                _dbContext.Admins.Add(new Admins {

                    UserName = username,
                    Password = password,
                    Role= role
                });
                await _dbContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> editUser(int id,string username, string password, string role)
        {
            var user = await _dbContext.Admins.AsNoTracking().FirstOrDefaultAsync(n => n.UserID==id);
            if (user == null)
            {
                return false;
            }
            else
            {
                if (user.UserName == username)
                {
                    user.Password = password;
                    user.Role = role;
                }
                else
                {
                    var user2= await _dbContext.Admins.AsNoTracking().FirstOrDefaultAsync(n => n.UserName == username);
                    if (user2 != null)
                        return false;
                    else {
                        user.UserName = username;
                        user.Password = password;
                        user.Role = role;
                    }
                }
                _dbContext.Update(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }
        }


        public async Task<List<Admins>> userList(string key = null)
        {
            Expression<Func<Admins, bool>> express = PredicateExtensions.True<Admins>();
            if (key != null)
            {
                express = express.And(n => n.UserName.Contains(key));
            }
            return await _dbContext.Admins.AsNoTracking().Where(express).OrderByDescending(r => r.RegDate).ToListAsync();
        }

        public async Task delUser(int id)
        {
            var user = await _dbContext.Admins.AsNoTracking().SingleOrDefaultAsync(m => m.UserID == id);
            if (user != null) {
                _dbContext.Admins.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Admins> getUser(int id)
        {
            var user = await _dbContext.Admins.AsNoTracking().SingleOrDefaultAsync(m => m.UserID == id);
            if (user != null)
            {
                return user;
            }
            else
                return null;
        }

    }
}
