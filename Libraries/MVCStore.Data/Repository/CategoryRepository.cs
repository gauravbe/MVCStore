using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCStore.Data.Context;
using MVCStore.Data.Entities;

namespace MVCStore.Data.Repository
{
    public class CategoryRepository : RepositoryBase<Category>
    {
        public CategoryRepository(IDatabaseFactory databaseFactory) : base(databaseFactory) { }

        public IQueryable<Category> Categories
        {
            get { return DataContext.Categories; }
        }

        public override void Update(Category category)
        {

            if (category.ID == 0)
            {
                DataContext.Categories.Add(category);
            }
            else
            {
                Category dbEntry = DataContext.Categories.Find(category.ID);
                if (dbEntry != null)
                {
                    dbEntry.Name = category.Name;
                    dbEntry.Description = category.Description;
                }
            }
            DataContext.SaveChanges();
        }

        public override void Delete(int categoryId)
        {
            Category dbEntry = DataContext.Categories.Find(categoryId);
            if (dbEntry != null)
            {
                DataContext.Categories.Remove(dbEntry);
                DataContext.SaveChanges();
            }
        }
    }
}
