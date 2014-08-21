using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCStore.Core.Data;
using MVCStore.Data.Entities;

namespace MVCStore.Services.Catalog
{
    public class CategoryService: ICategoryService
    {
        private readonly IRepository<Category> _categorRepository; 

        public CategoryService(IRepository<Category> categoryRepository)
        {
            this._categorRepository = categoryRepository;
        }
        
        public void SaveCategory(Category category)
        {
            _categorRepository.Update(category);
        }

        public IEnumerable<Category> FetchCategories()
        {
           return _categorRepository.GetAll();
        }
    }
}
