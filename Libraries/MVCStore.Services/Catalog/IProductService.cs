using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCStore.Data.Entities;

namespace MVCStore.Services.Catalog
{
    public interface IProductService
    {
        void SaveProduct(HttpPostedFileBase file, Product product);
        IEnumerable<Product> FetchProducts();
        Product GetProduct(int id);
        void Delete(int id);
        byte[] GetImageFromDataBase(int id);
    }
}
