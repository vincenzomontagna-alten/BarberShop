using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLayer.Models;

namespace DBLayer.Interfaces
{
    public interface IProductRepository
    {
        Product GetProduct(int id);

        List<Product> GetProducts();

        void UpdateProduct(Product product);

        void DeleteProduct(int id);

        void AddProduct(Product product);
    }
}
