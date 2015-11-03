using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbBench.Common;
using DbLib.Dapper;

namespace DbBench
{
    public class DataAccessFactory
    {
        /// <summary>
        /// Static Factory Method, ORM type selection delegated to this class
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IDataAccess GetDataLib(OrmTypes type, string connString)
        {
            switch (type)
            {
                    case OrmTypes.BareMetal:
                        return new AdoDb(connString);
                    case OrmTypes.Dapper:
                        return new DapperDb(connString);
                    case OrmTypes.EntityFramework:
                        return new EfDb(connString);
                default:
                    throw new Exception("ORM Type not specified!");
            }
        }
    }

    public enum OrmTypes
    {
        EntityFramework,
        Dapper,
        BareMetal
    }
}
