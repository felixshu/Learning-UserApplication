using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Application.Service.DataContext;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using User.Data.Model;
using UserApp.WebUI.Models;

namespace UserApp.WebUI
{
	public class EmailService : IIdentityMessageService
	{
		public Task SendAsync(IdentityMessage message)
		{
			// Plug in your email service here to send an email.
			const string userName = "Graysonshoppingmall";
			const string password = "Y8Bcg>tCs\"YiP\"4rKw7LpsCVs";
			const string from = "prc.shuyun@gmail.com";
			const int port = 587;

			var smtpClient = new SmtpClient
			{
				Host = "smtp.sendgrid.net",
				Port = port,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				EnableSsl = true,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(userName, password)
			};

			var mailMessage = new MailMessage(from, message.Destination)
			{
				Subject = message.Subject,
				Body = message.Body
			};

			return smtpClient.SendMailAsync(mailMessage);
		}
	}

	public class SmsService : IIdentityMessageService
	{
		public Task SendAsync(IdentityMessage message)
		{
			// Plug in your SMS service here to send a text message.
			return Task.FromResult(0);
		}
	}

	// Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
	public class ApplicationUserManager : UserManager<AppUser>
	{
		public ApplicationUserManager(IUserStore<AppUser> store)
			: base(store)
		{
		}

		public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
			IOwinContext context)
		{
			var manager = new ApplicationUserManager(new UserStore<AppUser>(context.Get<AppUserDbContext>()));
			// Configure validation logic for usernames
			manager.UserValidator = new UserValidator<AppUser>(manager)
			{
				AllowOnlyAlphanumericUserNames = false,
				RequireUniqueEmail = true
			};

			// Configure validation logic for passwords
			manager.PasswordValidator = new PasswordValidator
			{
				RequiredLength = 6,
				RequireNonLetterOrDigit = true,
				RequireDigit = true,
				RequireLowercase = true,
				RequireUppercase = true,
			};

			// Configure user lockout defaults
			manager.UserLockoutEnabledByDefault = true;
			manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
			manager.MaxFailedAccessAttemptsBeforeLockout = 5;

			// Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
			// You can write your own provider and plug it in here.
			manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<AppUser>
			{
				MessageFormat = "Your security code is {0}"
			});
			manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<AppUser>
			{
				Subject = "Security Code",
				BodyFormat = "Your security code is {0}"
			});
			manager.EmailService = new EmailService();
			manager.SmsService = new SmsService();
			var dataProtectionProvider = options.DataProtectionProvider;
			if (dataProtectionProvider != null)
			{
				manager.UserTokenProvider =
					new DataProtectorTokenProvider<AppUser>(dataProtectionProvider.Create("ASP.NET Identity"));
			}
			return manager;
		}
	}

	// Configure the application sign-in manager which is used in this application.
	public class ApplicationSignInManager : SignInManager<AppUser, string>
	{
		public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
			: base(userManager, authenticationManager)
		{
		}

		public override Task<ClaimsIdentity> CreateUserIdentityAsync(AppUser user)
		{
			return user.GenerateUserIdentityAsync((ApplicationUserManager) UserManager);
		}

		public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options,
			IOwinContext context)
		{
			return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
		}
	}

	//Configure the Role Manager used in the application.
	public class ApplicationRoleManager : RoleManager<AppUserRole>
	{
		public ApplicationRoleManager(IRoleStore<AppUserRole, string> store) : base(store)
		{
		}

		public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options,
			IOwinContext context)
		{
			return new ApplicationRoleManager(new RoleStore<AppUserRole>(context.Get<AppUserDbContext>()));
		}
	}
}