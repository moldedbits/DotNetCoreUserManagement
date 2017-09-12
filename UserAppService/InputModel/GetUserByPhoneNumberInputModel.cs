namespace UserAppService.InputModel
{
    public class GetUserByPhoneNumberInputModel : InputBaseModel
    {
        public string PhoneNumber { get; set; }

        public override void Validate(BaseAPIController controller, string methodName)
        {
            base.Validate(controller, methodName);
            RequireInput(PhoneNumber, ResourceFiles.LocalizedText.RequirePhoneNumber);
        }
    }
}
