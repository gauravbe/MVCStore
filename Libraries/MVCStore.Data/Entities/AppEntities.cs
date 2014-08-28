using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCStore.Data.Entities
{
    public class AppEntities : DbContext
    {
        public AppEntities() : base() { }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}
