using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbBench.Common;
using DbLib.ADO.Properties;

namespace DbBench
{
    public class AdoDb : IDataAccess
    {
        private readonly string connString;

        public AdoDb(string connectionString)
        {
            this.connString = connectionString;
        }

        public IEnumerable<Product> Read(int numberOfProducts)
        {
            string query = string.Format("SELECT TOP {0} [ProductName],[ProductDescription],[Quantity],[IsOnSale] FROM BenchDb ORDER BY ProductName", numberOfProducts);
            using (var connection = new SqlConnection(connString))
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
            string query = string.Format("INSERT INTO BenchDb (ProductName,ProductDescription,Quantity,IsOnSale) VALUES({0},{1},{2},{3})", product.ProductName, product.ProductDescription, product.Quantity, product.IsOnSale);
            using (var connection = new SqlConnection(connString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
