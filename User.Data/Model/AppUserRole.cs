using System;
using System.Threading;
using Microsoft.AspNet.Identity.EntityFramework;

namespace User.Data.Model
{
	public class AppUserRole : IdentityRole
	{
		public AppUserRole()
		{
			this.Id = Guid.NewGuid().ToString();
		}

		public AppUserRole(string name) : base(name)
		{
			this.Name = name;
		}
	}
}