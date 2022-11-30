using bulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using bulkyBookWeb.Data;
using Microsoft.Data.SqlClient;

namespace bulkyBookWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //private readonly crawlerDbContext _db;

        //public HomeController(crawlerDbContext db)
        //{
        //    _db = db;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult tommyhilfiger() 
        {
            //scrapper();
            return View();
        }

        ////Get
        //public IActionResult Edit(int? id)
        //{
        //    return View(cnn);
        //}
        ////Post
        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public IActionResult Edit(Catagory obj)
        //{
        //    _db.Catagories.Update(obj);
        //    _db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public static void scrapper()
        {
            tommyHilfiger.tommyhilfiger_Data_Process(0,1);
        }
    }
}