using System.Threading;
using Microsoft.AspNet.Identity.EntityFramework;

namespace User.Data.Model
{
	public class AppUserRole:IdentityRole
	{
		public AppUserRole()
		{
			
		}public AppUserRole(string name):base(name)
		{
			
		}
	}
}