using MVCStore.Core.Authentication;
using MVCStore.Core.Data;
using MVCStore.Data;
using MVCStore.Data.Entities;
using MVCStore.Data.Repository;
using MVCStore.Services.Authentication;
using MVCStore.Services.Catalog;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;



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
            kernel.Bind<IRepository<Product>>().To<ProductRepository>();
            kernel.Bind<IProductService>().To<ProductService>();
            kernel.Bind<IAuthenticationService>().To<AspNetAuthenticationService>();
            kernel.Bind<IMembership>().To<AspNetMembershipRepository>();
            kernel.Bind<ICategoryService>().To<CategoryService>();
            kernel.Bind<IRepository<Category>>().To<CategoryRepository>();
            kernel.Bind<IDatabaseFactory>().To<DatabaseFactory>();
        }
    }
}