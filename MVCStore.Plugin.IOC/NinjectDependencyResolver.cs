using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MVCStore.Core.Data;
using MVCStore.Data.Entities;
using MVCStore.Data.Repository;
using MVCStore.Services.Authentication;
using MVCStore.Services.Catalog;
using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;
using MVCStore.Services;
using MVCStore.Data;
using MVCStore.Core.Authentication;



namespace MVCStore.Plugin.IOC
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            //Add bindings sample .Bind<Interface>().To<Class>()
            //kernel.Bind<IProductRepository>().To<ProductRepository>();
            kernel.Bind<IAuthenticationService>().To<AspNetAuthenticationService>();
            kernel.Bind<IMembership>().To<AspNetMembershipRepository>();
            kernel.Bind<ICategoryService>().To<CategoryService>();
            kernel.Bind<IRepository<Category>>().To<CategoryRepository>();
            kernel.Bind<IDatabaseFactory>().To<DatabaseFactory>();
            

        }
    }
}
