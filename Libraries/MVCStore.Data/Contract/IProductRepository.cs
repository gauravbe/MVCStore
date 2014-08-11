using System;
using System.Collections.Generic;
using System.Linq;
using MVCStore.Data.Entities;

namespace MVCStore.Data.Contract
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        void SaveProduct(Product product);
        Product DeleteProduct(int productId);
    }
}
