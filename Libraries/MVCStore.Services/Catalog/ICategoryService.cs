using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCStore.Data.Entities;

namespace MVCStore.Services.Catalog
{
    public interface ICategoryService
    {
        void SaveCategory(Category category);
        IEnumerable<Category> FetchCategories();
    }
}
