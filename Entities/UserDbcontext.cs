using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class UserDbcontext : DbContext
    {
        public UserDbcontext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<WhiteLabelTenant> WhiteLabelTenants { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<WhiteLabelTenant>().ToTable("WhiteLabelTenants").HasIndex(t => t.Slug).IsUnique();
        }
    }
}
