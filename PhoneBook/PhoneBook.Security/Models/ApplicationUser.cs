using Microsoft.AspNetCore.Identity;

namespace PhoneBook.Security.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
    }
}
