using UserAppService.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using UserAppService.AuthenticationManagers;
using Microsoft.AspNet.Identity;
using System.Data.Common;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using UserAppService.Interfaces;
using System.Threading;
using System.Text;
using System.Data.Entity.Validation;

namespace UserAppService.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, ApplicationRole, int,
    UserLogin, UserRole, UserClaim>
    {
        public ApplicationDbContext() : base("UserManagementConnection")
        {
            Database.SetInitializer(new ApplicationDbInitializer());

        }

        public ApplicationDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Database.SetInitializer(new ApplicationDbInitializer());
        }

        public ApplicationDbContext(DbConnection connection) : base(connection, true)
        {
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public int UserId { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if(modelBuilder == null)
            {
                throw new ArgumentNullException("ModelBuilder is NULL");
            }

           modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

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
            modelBuilder.Entity<ApplicationRole>().HasRequired(x => x.CreatedBy).WithMany().HasForeignKey(x => x.CreatedById).WillCascadeOnDelete(false);
            modelBuilder.Entity<ApplicationRole>().HasRequired(x => x.UpdatedBy).WithMany().HasForeignKey(x => x.UpdatedById).WillCascadeOnDelete(false);

            modelBuilder.Entity<Platform>().HasRequired(x => x.CreatedBy).WithMany().HasForeignKey(x => x.CreatedById).WillCascadeOnDelete(false);
            modelBuilder.Entity<Platform>().HasRequired(x => x.UpdatedBy).WithMany().HasForeignKey(x => x.UpdatedById).WillCascadeOnDelete(false);

            modelBuilder.Entity<UserRole>().HasRequired(x => x.CreatedBy).WithMany().HasForeignKey(x => x.CreatedById).WillCascadeOnDelete(false);
            modelBuilder.Entity<UserRole>().HasRequired(x => x.UpdatedBy).WithMany().HasForeignKey(x => x.UpdatedById).WillCascadeOnDelete(false);
        }

        public override int SaveChanges()
        {
            try
            {
                var entities = new List<Tuple<DbEntityEntry, EntityState>>();
                var entries = ChangeTracker.Entries().ToList();

                var modifiedEntries = entries.Where(x => x.Entity is IEntity && x.State == EntityState.Added || x.State == EntityState.Modified);

                foreach (var entry in modifiedEntries)
                {
                    var entity = entry.Entity as IEntity;
                    if (entity == null) continue;

                    //need to set the default user as system if the current logged in user is empty, meaning the path was arrived at by non-authenticated api's which are used for registration.
                    UserId = string.IsNullOrWhiteSpace(Thread.CurrentPrincipal.Identity.GetUserId()) ? -1 : UserId;

                    var now = DateTime.UtcNow;

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedById = UserId;
                        entity.CreatedOn = now;

                        entity.UpdatedById = UserId;
                        entity.UpdatedOn = now;
                    }
                    else
                    {
                        entity.UpdatedById = UserId;
                        entity.UpdatedOn = now;
                    }

                    entities.Add(new Tuple<DbEntityEntry, EntityState>(entry, entry.State));
                }

                var result = base.SaveChanges();

                return result;
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
            catch (InvalidOperationException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                System.Diagnostics.Debug.Write("");
                throw ex;
            }
            finally
            {
            }
        }

        public bool Seed(ApplicationDbContext context)
        {
#if DEBUG
            bool success = false;

            //ApplicationRoleManager _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole, int, UserRole>(context));

            //success = this.CreateRole(_roleManager, "Admin", "Global Access");
            //if(!success == true) return success;

            //success = this.CreateRole(_roleManager, "CanEdit", "Edit existing records");
            //if(!success == true) return success;

            //success = this.CreateRole(_roleManager, "User", "Restricted to business domain activity");
            //if(!success) return success;

            //// Create my debug (testing) objects here

            //ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<User, ApplicationRole, int, UserLogin, UserRole, UserClaim>(context));

            //User user = new User();
            //PasswordHasher passwordHasher = new PasswordHasher();

            //user.UserName = "youremail@testemail.com";
            //user.Email = "youremail@testemail.com";

            //IdentityResult result = userManager.Create(user, "Pass@123");

            //success = this.AddUserToRole(userManager, user.Id, "Admin");
            //if(!success) return success;

            //success = this.AddUserToRole(userManager, user.Id, "CanEdit");
            //if(!success) return success;

            //success = this.AddUserToRole(userManager, user.Id, "User");
            //if(!success) return success;

            return success;
#endif
        }

        public bool RoleExists(ApplicationRoleManager roleManager, string name)
        {
            return roleManager.RoleExists(name);
        }

        //public bool CreateRole(ApplicationRoleManager _roleManager, string name, string description = "")
        //{
        //    var idResult = _roleManager.Create(new ApplicationRole(name, description));
        //    return idResult.Succeeded;
        //}

        //public bool AddUserToRole(ApplicationUserManager _userManager, int userId, string roleName)
        //{
        //    var idResult = _userManager.AddToRole<User, int>(userId, roleName);
        //    return idResult.Succeeded;
        //}

        //public void ClearUserRoles(ApplicationUserManager userManager, int userId)
        //{
        //    var user = userManager.FindById(userId);

        //    var currentRoles = new List<UserRole>();

        //    currentRoles.AddRange(user.UserRoles);

        //    foreach(UserRole role in currentRoles)
        //    {
        //        userManager.RemoveFromRole(userId, role.Role.Name);
        //    }
        //}

        //public void RemoveFromRole(ApplicationUserManager userManager, int userId, string roleName)
        //{
        //    userManager.RemoveFromRole(userId, roleName);
        //}

        //public void DeleteRole(ApplicationDbContext context, ApplicationUserManager userManager, int roleId)
        //{
        //    var roleUsers = context.Users.Where(u => u.UserRoles.Any(r => r.RoleId == roleId));
        //    var role = context.Roles.Find(roleId);

        //    foreach(var user in roleUsers)
        //    {
        //        this.RemoveFromRole(userManager, user.Id, role.Name);
        //    }
        //    context.Roles.Remove(role);
        //    context.SaveChanges();
        //}

        public DbSet<Platform> Platform { get; set; }

        public DbSet<UserRole> UserRole { get; set; }
    }
}