using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Contract.category
{
    public class CategoryDBContext : DbContext
    {
        public CategoryDBContext(DbContextOptions<CategoryDBContext> options) : base(options)
        {

        }
        public virtual DbSet<Category> Categorys { get; set; }


    }
}
