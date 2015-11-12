using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace User.Data.Model
{
	public class AppUser : IdentityUser
	{
		public AppUser()
		{
			this.Id = Guid.NewGuid().ToString();
		}
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			return userIdentity;
		}
	}
}