using bulkyBookWeb.Data;
using bulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace bulkyBookWeb.Controllers
{
    

    public class CatagoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CatagoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Catagory> objCatagoryList = _db.Catagories;
            return View(objCatagoryList); 
        }
        //Get
        public IActionResult Create()
        {
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Catagory obj)
        {
            _db.Catagories.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
