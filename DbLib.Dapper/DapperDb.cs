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

        public IEnumerable<Product> Read(int numberOfProducts)
        {
            IEnumerable<Product> data;
            string query = string.Format("SELECT TOP {0} [ProductName],[ProductDescription],[Quantity],[IsOnSale] FROM BenchDb ORDER BY ProductName", numberOfProducts);
            using (var conn = new SqlConnection(connString))
            {
                data = conn.Query<Product>(query);
            }

            return data;
        }

        public void Insert(Product product)
        {
            const string query = "INSERT INTO BenchDb (ProductName,ProductDescription,Quantity,IsOnSale) VALUES(@ProductName,@ProductDescription,@Quantity,@IsOnSale)";
            using (var conn = new SqlConnection(connString))
            {
                conn.Query(query, product);
            }
        }
    }
}
