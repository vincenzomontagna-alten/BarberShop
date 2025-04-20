using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLayer.Data;
using DBLayer.Interfaces;
using DBLayer.Models;

namespace DBLayer.Repositories
{
    public class ProductRepository : IProductRepositoy
    {
        private readonly Context _context;
        public ProductRepository() { }

        public ProductRepository(Context context) 
        {
            _context = context;
        }
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }


        public void DeleteProduct(int id)
        {
            Product productToDelete = GetProduct(id);
            _context.Products.Remove(productToDelete);
            _context.SaveChanges();
        }

        public Product GetProduct(int id)
        {
            return _context.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public void UpdateProduct(Product product)
        {
            Product productToUpdate = GetProduct(product.Id);
            productToUpdate.Nome = product.Nome;
            productToUpdate.Quantita = product.Quantita;
            productToUpdate.QuantitaMinimaRichiesta = product.QuantitaMinimaRichiesta;
            _context.SaveChanges();
        }
    }
}
