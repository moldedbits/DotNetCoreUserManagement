using Microsoft.AspNetCore.Identity;

namespace UserAppService.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class UserInputModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
