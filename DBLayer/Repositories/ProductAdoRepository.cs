using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLayer.Interfaces;
using DBLayer.Models;
using Microsoft.Data.SqlClient;

namespace DBLayer.Repositories
{
    public class ProductAdoRepository : IProductRepository
    {
        private readonly string _connectionString = "Server = localhost; Database = BarberShop; Trusted_Connection = True; TrustServerCertificate = True";
        public void AddProduct(Product product)
        {
            string query = "insert into prodotto values (@productName, @productQuantity, @productMinimumQuantity)";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            { 
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@productName", product.Nome);
                    command.Parameters.AddWithValue("@productQuantity", product.Quantita);
                    command.Parameters.AddWithValue("@productMinimumQuantity", product.QuantitaMinimaRichiesta);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProduct(int id)
        {
            string query = "delete from Prodotto where Id = @productId";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("productId", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Product GetProduct(int id)
        {
            
            string query = "select * from Prodotto where Id = @productId";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("productId", id);
                    var reader = command.ExecuteReader();
                    Product product = new Product(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2));
                    return product;
                }
            }
        }

        public List<Product> GetProducts()
        {
            string query = "select * from Prodotto ";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    List<Product> products = new List<Product>();   
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Product product = new Product(reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3));
                        products.Add(product);                   
                    }
                    return products;
                }
            }
        }

        public void UpdateProduct(Product product)
        {
            string query = "UPDATE Prodotto SET Nome = @productName, Quantita = @productQuantity, QuantitaMinimaRichiesta = @productMinimumQuantity WHERE Id = @productId;";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("productId", product.Id);
                    command.Parameters.AddWithValue("productName", product.Nome);
                    command.Parameters.AddWithValue("productQuantity", product.Quantita);
                    command.Parameters.AddWithValue("productMinimumQuantity", product.QuantitaMinimaRichiesta);
                    var reader = command.ExecuteNonQuery();
                    
                }
            }
        }
    }
}
