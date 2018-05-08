using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sibri.Contract.pages
{
    public class PageDBContext : DbContext
    {
        public PageDBContext(DbContextOptions<PageDBContext> options) : base(options)
        {

        }
        public virtual DbSet<Pages> Pages { get; set; }
    }
}
