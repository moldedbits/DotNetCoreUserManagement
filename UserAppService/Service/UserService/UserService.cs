//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using UserAppService.Context;
//using UserAppService.InputModel;
//using UserAppService.Models;
//using UserAppService.OutputModels;
//using UserAppService.Repositories;

//namespace UserAppService.Service.UserService
//{
//    public class UserService : IUserService
//    {
//        private ApplicationUserManager _userManager;
//        private readonly IRepository<User> _user;
//        private readonly ApplicationDbContext _applicationDbContext;

//        public UserService(ApplicationUserManager userManager)
//        {
//            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
//            _applicationDbContext = applicationDbContext;
//            _user = new Repository<User>(_applicationDbContext);
//            _userManager = userManager;
//        }

//        public BoolMethodResult RegisterUser(UserRegisterInputModel inputModel)
//        {
//            var user = new User() { UserName = inputModel.Email, Email = inputModel.Email };
//            IdentityResult result = _userManager.Create(user, inputModel.Password);

//            if( !result.Succeeded )
//                throw new Exception(ResourceFiles.LocalizedText.UserRegisterFail + result.Errors.First());

//            return new BoolMethodResult { WasSuccessful = true, Status = ResourceFiles.LocalizedText.UserRegister };
//        }

//        public void ForgotPassword(ForgotPasswordInputModel inputModel)
//        {
//            var user = VerifyUserByEmail(inputModel.Email);
//        }

//        public BoolMethodResult ConfirmEmailRequest(ConfirmEmailRequestInputModel inputModel)
//        {
//            var user = GetUserByEmail(inputModel.Email);

//            string code = _userManager.GenerateEmailConfirmationToken(user.Id);

//            var emailBody = ResourceFiles.LocalizedText.ConfirmEmailBody;
//            emailBody += $"{ConfigurationManager.AppSettings [ "BaseSiteURL" ]}{"confirm-account"}?code?{code}";

//            _userManager.SendEmail(user.Id, ResourceFiles.LocalizedText.ConfirmEmailSubject, emailBody);

//            return new BoolMethodResult { WasSuccessful = true, Status = ResourceFiles.LocalizedText.ConfirmationEmailSent};
//        }

//        public BoolMethodResult ConfirmEmail(ConfirmEmailInputModel inputModel)
//        {
//            var user = GetUserByEmail(inputModel.Email);
//            IdentityResult result = _userManager.ConfirmEmail(user.Id, inputModel.Code);

//            return new BoolMethodResult { WasSuccessful = true, Status = ResourceFiles.LocalizedText.EmailConfirmed };
//        }

//        public BoolMethodResult ResetPasswordRequest(ResetUserPasswordRequestInputModel inputModel)
//        {
//            var user = GetUserByEmail(inputModel.Email);

//            var callbackUrl = $"{ConfigurationManager.AppSettings [ "BaseSiteURL" ]}?{"password-reset"}?aid={user.AlternateId}";
//            _userManager.SendEmail(user.Id, ResourceFiles.LocalizedText.PasswordResetEmailSubject, ResourceFiles.LocalizedText.PasswordResetEmailBody + callbackUrl + "\">here</a>");

//            return new BoolMethodResult { WasSuccessful = true, Status = ResourceFiles.LocalizedText.PasswordResetRequest};
//        }

//        public BoolMethodResult ResetPassword(ResetUserPasswordInputModel inputModel)
//        {
//            var user = GetUserByAlternateId(inputModel.AlternateId);

//            user.AlternateId = Guid.NewGuid().ToString("N");
//            var result = _userManager.ResetPassword(user.Id, _userManager.GeneratePasswordResetToken(user.Id), inputModel.NewPassword);

//            //Reverting changes back to original when password-reset is not succesful
//            if( !result.Succeeded )
//            {
//                user.AlternateId = inputModel.AlternateId;
//            }

//            _user.InsertOrUpdate(user);
//            _user.Commit();

//            return new BoolMethodResult { WasSuccessful = true};
//        }

//        public async Task<User> VerifyUserByEmail(string email)
//        {
//            var user = await _userManager.FindByNameAsync(email);
//            if( user == null || !(await _userManager.IsEmailConfirmedAsync(user.Id)) )
//            {
//                throw new Exception(ResourceFiles.LocalizedText.UserNotExistsOrConfirmed);
//            }

//            return user;
//        }

//        public User GetUserByEmail(string email)
//        {
//            var user = _user.FirstOrDefault(x => x.Email == email);
//            if( user == null )
//            {
//                throw new Exception(ResourceFiles.LocalizedText.UserNotExists);
//            }
//            return user;
//        }

//        public User GetUserByAlternateId(string alternateId)
//        {
//            var user = _user.FirstOrDefault(x => x.AlternateId == alternateId);
//            if( user == null )
//            {
//                throw new Exception(ResourceFiles.LocalizedText.UserNotExists);
//            }
//            return user;
//        }

//        public List<User> GetAllUsers()
//        {
//            return _user.GetAll().ToList();
//        }

//        public User GetUserByPhoneNumber(GetUserByPhoneNumberInputModel inputModel)
//        {
//            var user = _user.GetAllQueryable(x => x.PhoneNumber == inputModel.PhoneNumber).SingleOrDefault();
//            return user;
//        }

//        public User GetUserByUserName(GetUserByUserNameInputModel inputModel)
//        {
//            var user = _user.GetAllQueryable(x => x.UserName == inputModel.UserName).SingleOrDefault();
//            return user;
//        }
//    }
//}
