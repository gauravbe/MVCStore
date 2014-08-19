using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCStore.Data.Entities;

namespace MVCStore.Data.Repository
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private AppEntities dataContext;
        public AppEntities Get()
        {
            return dataContext ?? (dataContext = new AppEntities());
        }

        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
