using System.ComponentModel.DataAnnotations;

namespace UserAppService.InputModels
{

    /// <summary>
    /// Log in input model
    /// </summary>
    public class AuthenticateUserInputModel
    {
        /// <summary>
        /// UserName is usually the user's email address
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Password for authentication
        /// </summary>
        [Required]
        public string Password { get; set; }

        //public override void Validate(BaseAPIController controller, string methodName)
        //{
        //    //base.Validate(controller, methodName);

        //    RequireInput(UserName, ResourceFiles.LocalizedText.RequireUserName);
        //    RequireInput(Password, ResourceFiles.LocalizedText.RequirePassword);

        //}

        //public override void GenerateHelpSampleData()
        //{
        //    base.GenerateHelpSampleData();
        //    UserName = "test@moldedbits.com";
        //}
    }


}