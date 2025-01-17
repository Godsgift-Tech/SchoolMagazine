using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolMagazine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Infrastructure.Data
{
    public class MagazineContext:  DbContext
    {
        public MagazineContext(DbContextOptions<MagazineContext> options) : base(options)
        {
        }

        public DbSet<School> Schools { get; set; }
        public DbSet<SchoolEvent> Events { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
