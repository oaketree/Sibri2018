using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sibri.Contract.category
{
    public class CategoryDBContext : DbContext
    {
        public CategoryDBContext(DbContextOptions<CategoryDBContext> options) : base(options)
        {

        }
        public virtual DbSet<Category> Categorys { get; set; }


    }
}
