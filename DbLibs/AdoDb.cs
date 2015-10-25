using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbLibs.Interfaces;
using DbLibs.Properties;
using DbLibs.Models;

namespace DbLibs
{
    public class AdoDb : IDataAccess
    {

        public IEnumerable<Product> Read(int numberOfProducts)
        {
            string query = string.Format("SELECT TOP {0} [ProductId],[ProductName],[ProductDescription],[Quantity],[IsOnSale] FROM BenchDb", numberOfProducts);
            using (var connection = new SqlConnection(Settings.Default.ProductContext))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Product()
                            {
                                ProductId = int.Parse(reader["ProductId"].ToString()),
                                ProductName = reader["ProductName"].ToString(),
                                ProductDescription = reader["ProductDescription"].ToString(),
                                Quantity = int.Parse(reader["Quantity"].ToString()),
                                IsOnSale = bool.Parse(reader["IsOnSale"].ToString())
                            };
                        }
                    }
                    connection.Close();
                }
            }
        }

        public void Insert(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
