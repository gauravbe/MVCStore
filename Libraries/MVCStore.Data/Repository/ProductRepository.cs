using System;
using System.Collections.Generic;
using System.Linq;
using MVCStore.Data.Context;
using MVCStore.Data.Contract;


namespace MVCStore.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Entities.Product> Products
        {
            get { return context.Products; }
        }

        public void SaveProduct(Entities.Product product)
        {
            throw new NotImplementedException();
        }

        public Entities.Product DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
