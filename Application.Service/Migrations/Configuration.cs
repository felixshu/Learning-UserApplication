using System;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;
using Application.Service.DataContext;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using User.Data.Model;

namespace Application.Service.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<AppUserDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(AppUserDbContext context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//

			/*User Initial in Database*/
			var userManager = new UserManager<AppUser>(new UserStore<AppUser>(new AppUserDbContext()));
			userManager.UserValidator = new UserValidator<AppUser>(manager:userManager)
			{
				AllowOnlyAlphanumericUserNames = false,
				RequireUniqueEmail = true,
			};

			var roleManager = new RoleManager<AppUserRole>(new RoleStore<AppUserRole>(new AppUserDbContext()));

			var name = "felixgrayson.sy@gmail.com";
			var password = "pwd123456";
			var firstname = "Felix";
			var lastname = "Grayson";
			var roleName = "GeneralManager";

			var role = roleManager.FindByName(roleName);

			if (role == null)
			{
				role = new AppUserRole(roleName);
				var roleResult = roleManager.Create(role);
			}

			var user = userManager.FindByName(name);
			if (user == null)
			{
				user = new AppUser
				{
					UserName = firstname + lastname,
					FirstName = firstname,
					LastName = lastname,
					Email = name
				};

				var result = userManager.Create(user, password);
				result = userManager.SetLockoutEnabled(user.Id, false);
			}

			//check the role has been assgined to a user
			var rolesForUser = userManager.GetRoles(user.Id);
			if (!rolesForUser.Contains(role.Name))
			{
				var result = userManager.AddToRole(user.Id, role.Name);
			}
		}
	}
}