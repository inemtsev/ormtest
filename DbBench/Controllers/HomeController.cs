using DbBench.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Diagnostics;
using DbLib.Common;

namespace DbBench.Controllers
{
    public class HomeController : Controller
    {
        private const string _alphabet = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public ActionResult Index()
        {
            Utility.ClearDb(Properties.Settings.Default.ProductContext);
            return View();
        }

        public ActionResult InsertEntries(int numberOfInserts, OrmTypes ormType)
        {
            var rnd = new Random();
            var sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < numberOfInserts; i++)
            {
                var chars  = Enumerable.Range(0, 100).Select(x => _alphabet[rnd.Next(0, _alphabet.Length)]);       //Generate some random characters to simulate actual data
                int number = rnd.Next(100,1000);

                IDataAccess currentRepo = DataAccessFactory.GetDataLib(ormType, Properties.Settings.Default.ProductContext);

                currentRepo.Insert(new Product()
                {
                    ProductDescription = new string(chars.ToArray<char>()),
                    ProductName = (new string(chars.ToArray<char>())).Substring(0,10),
                    Quantity = number,
                    IsOnSale = false
                });
            }

            sw.Stop();

            return Json(new { time = sw.ElapsedMilliseconds.ToString() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadEntries(int numberOfReads, OrmTypes ormType)
        {
            var sw = new Stopwatch();
            sw.Start();

            IDataAccess currentRepo = DataAccessFactory.GetDataLib(ormType, Properties.Settings.Default.ProductContext);
            var results = new List<Product>();

            for (int i = 1; i <= numberOfReads; i++)
            {
                var rnd = new Random();     //This randomizes the ID of the searched Product

                results.Add(currentRepo.Read(rnd.Next(1, numberOfReads)));
            }

            sw.Stop();

            return Json(new { time = sw.ElapsedMilliseconds.ToString(), count = results.Count },JsonRequestBehavior.AllowGet);
         }
    }
}