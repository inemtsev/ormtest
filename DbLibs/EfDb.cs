using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using DbBench.Interfaces;
using DbBench.Models;
using DbBench.Properties;

namespace DbBench
{
    public class EfDb : IDataAccess
    {
        readonly ProductContext _productsDb = new ProductContext();


        public IEnumerable<Product> Read(int numberOfProducts)
        {
            var products = _productsDb.Products.Take(numberOfProducts).ToList();
            return products;
        }

        public void Insert(Product product)
        {
            _productsDb.Products.Add(product);
        }
    }

    public class ProductContext : DbContext
    {
        public ProductContext()
            : base(Settings.Default.ProductContext)
        {   
        }

        public DbSet<Product> Products { get; set; }
    }
}
