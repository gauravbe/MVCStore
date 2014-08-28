using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCStore.Data.Entities;

namespace MVCStore.Data.Repository
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IQueryable<Product> Products
        {
            get { return DataContext.Products; }
        }

        public override void Update(Product product)
        {

            if (product.ProductID == 0)
            {
                DataContext.Products.Add(product);
            }
            else
            {
                Product dbEntry = DataContext.Products.Find(product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.CategoryId = product.CategoryId;
                    dbEntry.Price = product.Price;
                    dbEntry.ImageData = product.ImageData;
                }
            }
            DataContext.SaveChanges();
        }

        public override void Delete(int productId)
        {
            Product dbEntry = DataContext.Products.Find(productId);
            if (dbEntry != null)
            {
                DataContext.Products.Remove(dbEntry);
                DataContext.SaveChanges();
            }
        }

        public override byte[] GetImageById(int id)
        {
            Product dbEntry = DataContext.Products.Find(id);
            byte[] imageData = dbEntry.ImageData;
            return imageData;
        }
    }
}