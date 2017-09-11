using UserAppService.Enums;

namespace UserAppService.Exceptions
{
    public class UserFriendlyException : System.Exception
    {
        public ErrorType ErrorCode { get; set; }

        public string ErrorKey { get; private set; }

        public UserFriendlyException() { }

        public UserFriendlyException(string errorKey, string message) : base(message)
        {
            ErrorKey = errorKey;
        }

        public UserFriendlyException(string errorKey, string message, System.Exception innerException)
            : base(message, innerException)
        {
            ErrorKey = errorKey;
        }
    }
}
