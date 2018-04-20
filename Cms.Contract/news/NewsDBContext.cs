using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Contract.news
{
    public class NewsDBContext: DbContext
    {
        public NewsDBContext(DbContextOptions<NewsDBContext> options) : base(options)
        {

        }
        public virtual DbSet<News> News { get; set; }


    }


}
