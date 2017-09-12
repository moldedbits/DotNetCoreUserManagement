using UserAppService.Models;
using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using UserAppService.Interfaces;
using System.Threading;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace UserAppService.Context
{
    public class ApplicationDbContext : IdentityDbContext<User,ApplicationRole,int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (modelBuilder == null)
            {
                throw new ArgumentNullException("ModelBuilder is NULL");
            }

            base.OnModelCreating(modelBuilder);

            //Defining the keys and relations
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<ApplicationRole>().ToTable("ApplicationRole");
            modelBuilder.Entity<User>().HasMany((User u) => u.UserRoles);
            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            modelBuilder.Entity<Platform>().ToTable("Platform");

            // Manage foreign key constraints (meta-data tagging)
            ////modelBuilder.Entity<ApplicationRole>().HasRequired(x => x.CreatedBy).WithMany().HasForeignKey(x => x.CreatedById).WillCascadeOnDelete(false);
            ////modelBuilder.Entity<ApplicationRole>().HasRequired(x => x.UpdatedBy).WithMany().HasForeignKey(x => x.UpdatedById).WillCascadeOnDelete(false);

            ////modelBuilder.Entity<Platform>().HasRequired(x => x.CreatedBy).WithMany().HasForeignKey(x => x.CreatedById).WillCascadeOnDelete(false);
            ////modelBuilder.Entity<Platform>().HasRequired(x => x.UpdatedBy).WithMany().HasForeignKey(x => x.UpdatedById).WillCascadeOnDelete(false);

            ////modelBuilder.Entity<UserRole>().HasRequired(x => x.CreatedBy).WithMany().HasForeignKey(x => x.CreatedById).WillCascadeOnDelete(false);
            ////modelBuilder.Entity<UserRole>().HasRequired(x => x.UpdatedBy).WithMany().HasForeignKey(x => x.UpdatedById).WillCascadeOnDelete(false);

        }

        public DbSet<Platform> Platform { get; set; }

        public DbSet<UserRole> UserRole { get; set; }
    }
}