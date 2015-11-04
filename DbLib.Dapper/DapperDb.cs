using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbBench.Common;
using Dapper;

namespace DbLib.Dapper
{
    public class DapperDb : IDataAccess
    {
        private readonly string connString;

        public DapperDb(string connectionString)
        {
            this.connString = connectionString;
        }

        public Product Read(int idToRead)
        {
            Product data;
            string query = string.Format("SELECT TOP 1 [ProductName],[ProductDescription],[Quantity],[IsOnSale] FROM Products WHERE ProductId = {0}", idToRead);
            using (var conn = new SqlConnection(connString))
            {
                data = conn.Query<Product>(query).Single();
            }

            return data;
        }

        public void Insert(Product product)
        {
            const string query = "INSERT INTO Products (ProductName,ProductDescription,Quantity,IsOnSale) VALUES(@ProductName,@ProductDescription,@Quantity,@IsOnSale)";
            using (var conn = new SqlConnection(connString))
            {
                conn.Query(query, product);
            }
        }
    }
}
