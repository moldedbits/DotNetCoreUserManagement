//using System.Threading.Tasks;
//using UserAppService.Context;
//using UserAppService.Models;
//using System.Security.Claims;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Http;

//namespace UserAppService.AuthenticationManagers
//{
//    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

//    public class ApplicationUserManager : UserManager<User>
//    {
//        public ApplicationUserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
//        {
//        }
//    }

//    public class ApplicationRoleManager : RoleManager<ApplicationRole>
//    {
//        public ApplicationRoleManager(IRoleStore<ApplicationRole> store, IEnumerable<IRoleValidator<ApplicationRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<ApplicationRole>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
//        {
//        }
//    }

//    //public class EmailService : IIdentityMessageService
//    //{
//    //    public async Task SendAsync(IdentityMessageExtended message)
//    //    {
//    //        // Plug in your email service here to send an email.

//    //        EmailLayerConfigurations.SendGridLayer sendGrid = new EmailLayerConfigurations.SendGridLayer();
//    //        await sendGrid.configSendGridasync(message);

//    //        EmailLayerConfigurations.SMTPLayer smtp = new EmailLayerConfigurations.SMTPLayer();
//    //        await smtp.configSmtpasync(message);
//    //    }

//    //}

//    //public class SmsService : IIdentityMessageService
//    //{
//    //    public Task SendAsync(IdentityMessage message)
//    //    {
//    //        // Plug in your sms service here to send a text message.
//    //        return Task.FromResult(0);
//    //    }
//    //}

//    public class ApplicationSignInManager : SignInManager<User>
//    {
//        //public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
//        //{
//        //}

//        //public override Task<ClaimsIdentity> CreateUserIdentityAsync(User identityUser)
//        //{
//        //    return identityUser.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
//        //}

//        //public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
//        //{
//        //    return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
//        //}
//        public ApplicationSignInManager(UserManager<User> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<User> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<User>> logger, IAuthenticationSchemeProvider schemes) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
//        {
//        }
//    }
//}
