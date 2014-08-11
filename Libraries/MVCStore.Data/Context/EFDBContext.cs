using System;
using System.Data.Entity;
using System.Linq;
using MVCStore.Data.Entities;

namespace MVCStore.Data.Context
{
    public class EFDbContext : DbContext
    {
        public IQueryable<Entities.Product> Products { get; set; }
    }
}
