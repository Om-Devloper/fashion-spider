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
        [HttpGet]
        public IActionResult Index()
        {        
            return View();
        }
        [HttpPost]
        public IActionResult productDetail(IFormCollection form)
        {
            scrapper();
            
            string value = form["value"];
            ViewBag.refId = value;
            return View();
            //return View(value);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public static void scrapper()
        {
            //tommyHilfiger.tommyhilfiger_Data_Process(0,1);
            levisCrawler.levis_Data_Process(0, 2);

        }
    }
}