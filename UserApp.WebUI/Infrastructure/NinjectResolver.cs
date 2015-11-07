using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Ninject;
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