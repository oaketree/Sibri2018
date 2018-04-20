using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Contract.upload
{
    public class UploadDBContext: DbContext
    {
        public UploadDBContext(DbContextOptions<UploadDBContext> options) : base(options)
        {

        }
        public virtual DbSet<Uploads> Uploads { get; set; }
    }
}
