using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DbBench.Models;

namespace DbBench.Interfaces
{
    public interface IDataAccess
    {
        IEnumerable<Product> Read(int numberOfProducts);

        void Insert(Product product);
    }
}
