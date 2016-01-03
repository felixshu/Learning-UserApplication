using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Application.Service.DBContext;
using GenericRepository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using Repository.Pattern.DataContext;
using Repository.Pattern.UnitOfWork;
using User.Data.Model;
using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

namespace UserApp.WebUI.Infrastructure
{
    internal class NinjectResolver : IDependencyResolver, System.Web.Http.Dependencies.IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectResolver() : this(new StandardKernel())
        {
        }

        public NinjectResolver(IKernel kernel)
        {
            this._kernel = kernel;
            AddBinding(_kernel);
        }

        private void AddBinding(IKernel container)
        {
            //Identity
            ConfigureIdentity(container);
            //Service
            ConfigureService(container);
            //Generic Repository
            ConfigureRepository(container);
        }

        private void ConfigureRepository(IKernel container)
        {
            container.Bind<IUnitOfWork>().To<UnitOfWork>();
            container.Bind<IDataContext>().To<DataContext>();
        }

        private void ConfigureService(IKernel container)
        {
            
        }

        private void ConfigureIdentity(IKernel container)
        {
            container.Bind<IUserStore<AppUser>>()
                .To<UserStore<AppUser>>()
                .WithConstructorArgument("context", context => _kernel.Get<AppUserDbContext>());

            container.Bind<IRoleStore<AppUserRole, string>>()
                .To<RoleStore<AppUserRole, string, IdentityUserRole>>()
                .WithConstructorArgument("context", context => _kernel.Get<AppUserDbContext>());

            container.Bind<UserManager<AppUser, string>>().ToSelf();
            container.Bind<RoleManager<AppUserRole, string>>().ToSelf();
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