﻿using UserAppService.Models;
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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace UserAppService.Context
{
    public class ApplicationDbContext : IdentityDbContext<User,ApplicationRole,int,UserClaim,UserRole,UserLogin, RoleClaim,UserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //this.Database.Migrate();
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
            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaim");
            modelBuilder.Entity<UserToken>().ToTable("UserToken");

            // Manage foreign key constraints (meta-data tagging)
            ////modelBuilder.Entity<ApplicationRole>().HasRequired(x => x.CreatedBy).WithMany().HasForeignKey(x => x.CreatedById).WillCascadeOnDelete(false);
            ////modelBuilder.Entity<ApplicationRole>().HasRequired(x => x.UpdatedBy).WithMany().HasForeignKey(x => x.UpdatedById).WillCascadeOnDelete(false);

            ////modelBuilder.Entity<Platform>().HasRequired(x => x.CreatedBy).WithMany().HasForeignKey(x => x.CreatedById).WillCascadeOnDelete(false);
            ////modelBuilder.Entity<Platform>().HasRequired(x => x.UpdatedBy).WithMany().HasForeignKey(x => x.UpdatedById).WillCascadeOnDelete(false);

            ////modelBuilder.Entity<UserRole>().HasRequired(x => x.CreatedBy).WithMany().HasForeignKey(x => x.CreatedById).WillCascadeOnDelete(false);
            ////modelBuilder.Entity<UserRole>().HasRequired(x => x.UpdatedBy).WithMany().HasForeignKey(x => x.UpdatedById).WillCascadeOnDelete(false);

        }

        public DbSet<UserRole> UserRole { get; set; }

        //public static async Task CreateUsers(IServiceProvider container, IConfiguration configuration)
        //{
        //    using (var serviceScope = container.GetRequiredService<IServiceScopeFactory>().CreateScope())
        //    {
        //        UserManager<User> userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
        //        RoleManager<ApplicationRole> roleManager = serviceScope.ServiceProvider.GetService<RoleManager<ApplicationRole>>();

        //        if (await userManager.FindByNameAsync(userName) == null)
        //        {
        //            if (await roleManager.FindByNameAsync(role) == null)
        //            {
        //                await roleManager.CreateAsync(new IdentityRole(role));
        //            }
        //            ApplicationUser user = new ApplicationUser
        //            {
        //                UserName = userName,
        //                Email = email
        //            };
        //            IdentityResult result = await userManager.CreateAsync(user, password);
        //            if (result.Succeeded)
        //            {
        //                await userManager.AddToRoleAsync(user, role);
        //            }
        //        }
        //    }
        //}
        //public static async Task CreateApplicationRoles(IServiceProvider container)
        //{
        //    using (var serviceScope = container.GetRequiredService<IServiceScopeFactory>().CreateScope())
        //    {
        //        RoleManager<ApplicationRole> roleManager = serviceScope.ServiceProvider.GetService<RoleManager<ApplicationRole>>();

        //        if (await roleManager.FindByNameAsync("Authors") == null)
        //        {
        //            IdentityResult result = await roleManager.CreateAsync(new IdentityRole("Authors"));
        //            if (!result.Succeeded)
        //            {
        //                throw new Exception("Error creating authors role!");
        //            }
        //        }
        //    }
        //}

    }

    public static class DbContextExtensions
    {
        public static void Seed(this ApplicationDbContext db)
        {
            Configuration.Seed(db); // <= WARNING HERE
        }
    }

    public static class DbContextSeedData
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>())
            {
                Configuration.Seed(context); 
            }
            
        }
    }
}