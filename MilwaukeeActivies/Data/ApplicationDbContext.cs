using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MilwaukeeActivies.Models;

namespace MilwaukeeActivies.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Activities> Activities { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Customer> Customers { get; set; }
        
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Interest> Interests { get; set; }
        
        
        protected override void OnModelCreating(ModelBuilder builder)

        

        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Customer", NormalizedName = "CUSTOMER" });
        }
    }
}
