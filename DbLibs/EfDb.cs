using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using DbBench.Common;
using DbBench.Properties;

namespace DbBench
{
    public class EfDb : IDataAccess
    {
        private readonly string connString;
        private readonly ProductContext _productsDb;

        public EfDb(string connectionString)
        {
            this.connString = connectionString;
            _productsDb = new ProductContext(connString);
        }

        

        public IEnumerable<Product> Read(int numberOfProducts)
        {
            var products = _productsDb.Products.OrderBy(x => x.ProductName).Take(numberOfProducts).ToList();
            return products;
        }

        public void Insert(Product product)
        {
            _productsDb.Products.Add(product);
        }
    }

    public class ProductContext : DbContext
    {
        public ProductContext(string connectionString)
            : base(connectionString)
        {   
        }

        public DbSet<Product> Products { get; set; }
    }
}
