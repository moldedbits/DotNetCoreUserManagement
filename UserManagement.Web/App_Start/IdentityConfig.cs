using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using UserAppService.Context;
using UserAppService.Models;

namespace UserManagement.Web
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    //public class ApplicationUserManager : UserManager<User, int>
    //{
    //    public ApplicationUserManager(IUserStore<User, int> store)
    //        : base(store)
    //    {
    //    }

    //    public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
    //    {
    //        var manager = new ApplicationUserManager(new UserStore<User, ApplicationRole, int, UserLogin, UserRole, UserClaim>(context.Get<ApplicationDbContext>()));
    //        // Configure validation logic for usernames
    //        manager.UserValidator = new UserValidator<User, int>(manager)
    //        {
    //            AllowOnlyAlphanumericUserNames = false,
    //            RequireUniqueEmail = true
    //        };
    //        // Configure validation logic for passwords
    //        manager.PasswordValidator = new PasswordValidator
    //        {
    //            RequiredLength = 6,
    //            RequireNonLetterOrDigit = true,
    //            RequireDigit = true,
    //            RequireLowercase = true,
    //            RequireUppercase = true,
    //        };
    //        // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
    //        // You can write your own provider and plug in here.
    //        manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<User, int>
    //        {
    //            MessageFormat = "Your security code is: {0}"
    //        });
    //        manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<User, int>
    //        {
    //            Subject = "Security Code",
    //            BodyFormat = "Your security code is: {0}"
    //        });
    //        manager.EmailService = new EmailService();
    //        manager.SmsService = new SmsService();
    //        var dataProtectionProvider = options.DataProtectionProvider;
    //        if(dataProtectionProvider != null)
    //        {
    //            manager.UserTokenProvider = new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("ASP.NET Identity"));
    //        }
    //        return manager;
    //    }
    //}

    //public class ApplicationRoleManager : RoleManager<ApplicationRole, int>
    //{
    //    public ApplicationRoleManager(IRoleStore<ApplicationRole, int> roleStore)
    //        : base(roleStore)
    //    {
    //    }

    //    public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
    //    {
    //        return new ApplicationRoleManager(new RoleStore<ApplicationRole, int, UserRole>(context.Get<ApplicationDbContext>()));
    //    }
    //}

    //public class EmailService : IIdentityMessageService
    //{
    //    public Task SendAsync(IdentityMessage message)
    //    {
    //        // Plug in your email service here to send an email.
    //        return Task.FromResult(0);
    //    }
    //}

    //public class SmsService : IIdentityMessageService
    //{
    //    public Task SendAsync(IdentityMessage message)
    //    {
    //        // Plug in your sms service here to send a text message.
    //        return Task.FromResult(0);
    //    }
    //}
}
