using Microsoft.EntityFrameworkCore;

namespace Sibri.Contract.pages
{
    public class NewsDBContext: DbContext
    {
        public NewsDBContext(DbContextOptions<NewsDBContext> options) : base(options)
        {

        }
        public virtual DbSet<News> News { get; set; }


    }


}
