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

        //Get
        public IActionResult Edit(int ? id)
        {
            return View(_db.Catagories.Find(id));
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Catagory obj)
        {
            _db.Catagories.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Get
        public IActionResult Delete(int? id)
        {
            return View(_db.Catagories.Find(id));
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete(Catagory obj)
        {
            _db.Catagories.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
