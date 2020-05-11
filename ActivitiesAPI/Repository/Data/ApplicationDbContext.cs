using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ActivityType> ActivityTypes { get; set; }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}
