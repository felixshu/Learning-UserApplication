using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Application.Service.DataContext;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using User.Data.Model;
using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

namespace UserApp.WebUI.Infrastructure
{
	internal class NinjectResolver : IDependencyResolver, System.Web.Http.Dependencies.IDependencyResolver
	{
		private readonly IKernel _kernel;

		public NinjectResolver():this(new StandardKernel())
		{
			
		}

		public NinjectResolver(IKernel kernel)
		{
			this._kernel = kernel;
			AddBinding();
		}

		private void AddBinding()
		{
			//Add Bindings here
			_kernel.Bind<IUserStore<AppUser>>()
				.To<UserStore<AppUser>>()
				.WithConstructorArgument("context",context=>_kernel.Get<AppUserDbContext>());

			_kernel.Bind<IRoleStore<AppUserRole, string>>()
				.To<RoleStore<AppUserRole,string, IdentityUserRole>>()
				.WithConstructorArgument("context", context=>_kernel.Get<AppUserDbContext>());

			_kernel.Bind<UserManager<AppUser,string>>().ToSelf();
			_kernel.Bind<RoleManager<AppUserRole,string>>().ToSelf();
		}

		public object GetService(Type serviceType)
		{
			return _kernel.TryGet(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return _kernel.GetAll(serviceType);
		}

		public IDependencyScope BeginScope()
		{
			return this;
		}

		public void Dispose()
		{
			//Do nothing here
		}
	}
}