using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MVCStore.Core.Data;
using MVCStore.Data.Entities;

namespace MVCStore.Services.Catalog
{
    public class ProductService: IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            this._productRepository = productRepository;
        }

        public void SaveProduct(HttpPostedFileBase file, Product product)
        {
            product.ImageData = ConvertToBytes(file);
            var productData = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageData = product.ImageData
            };

            _productRepository.Update(productData);
        }

        public IEnumerable<Product> FetchProducts()
        {
            return _productRepository.GetAll();
        }


        public Product GetProduct(int id)
        {
            return _productRepository.GetById(id);
        }


        public void Delete(int id)
        {
            _productRepository.Delete(id);
        }

        private byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }


        public byte[] GetImageFromDataBase(int id)
        {
            return _productRepository.GetImageById(id);
        }
    }
}
