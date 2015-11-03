﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DbBench.Common
{
    public interface IDataAccess
    {
        IEnumerable<Product> Read(int numberOfProducts);

        void Insert(Product product);
    }
}