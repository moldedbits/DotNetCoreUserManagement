namespace UserAppService.InputModel
{
    public class ExternalLoginConfirmationInputModel: InputBaseModel
    {
        public string Email { get; set; }

        public string ReturnUrl { get; set; }
    }
}
