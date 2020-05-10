using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using PhoneBook.Security.Models;

namespace PhoneBook.Security.Quickstart.Account
{
    public class ProfileService : IProfileService
    {
        protected UserManager<ApplicationUser> _userManager;

        public ProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //>Processing
            var user = await _userManager.GetUserAsync(context.Subject);

            var claims = new List<Claim>();
            if (user.Role != null)
            {
                claims.Add(new Claim("role", user.Role));
            }
            if (user.Login != null)
            {
                claims.Add(new Claim("name", user.Login));
            }
            if (user.DisplayName != null)
            {
                claims.Add(new Claim("displayName", user.DisplayName));
            }

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            //>Processing
            var user = await _userManager.GetUserAsync(context.Subject);

            context.IsActive = (user != null);
        }
    }
}
