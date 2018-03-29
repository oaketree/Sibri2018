using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Contract.login
{
    public class LoginDBContext : DbContext
    {
        public LoginDBContext(DbContextOptions<LoginDBContext> options) : base(options)
        {
            
        }
        public virtual DbSet<Admins> Admins { get; set; }
    }
}
