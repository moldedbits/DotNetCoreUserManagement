namespace UserAppService.InputModel
{
    public class ExternalLoginInputModel : InputBaseModel
    {
        public string Provider { get; set; }

        public string ReturnUrl { get; set; }

    }
}
