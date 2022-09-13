using Microsoft.AspNetCore.Mvc;

namespace bulkyBookWeb.Controllers
{
    public class CategoryController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
