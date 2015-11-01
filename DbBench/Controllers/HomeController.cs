using DbBench.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using DbBench.Models;

namespace DbBench.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataAccess currentRepo;
        private const string _alphabet = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public HomeController(IDataAccess repo)
        {
            this.currentRepo = repo;
        }

        public HomeController()
        {  }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InsertEntries(int numberOfEntries)
        {
            var rnd = new Random();
            var sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < numberOfEntries; i++)
            {
                var chars  = Enumerable.Range(0, 100)
                           .Select(x => _alphabet[rnd.Next(0, _alphabet.Length)]);       //Generate some random characters to simulate actual data
                int number = rnd.Next(100,1000);

                currentRepo.Insert(new Product()
                {
                    ProductId = Guid.NewGuid(),
                    ProductDescription = new string(chars.ToArray<char>()),
                    ProductName = (new string(chars.ToArray<char>())).Substring(0,10),
                    Quantity = number,
                    IsOnSale = false
                });
            }

            sw.Stop();

            return Json(new {time = sw.ElapsedMilliseconds});
        }

        public ActionResult ReadEntries(int numberOfEntries)
        {
            var sw = new Stopwatch();
            sw.Start();

            IEnumerable<Product> itemsFromDb = currentRepo.Read(numberOfEntries);

            sw.Stop();

            return Json(new { time = sw.ElapsedMilliseconds });
        }
    }
}