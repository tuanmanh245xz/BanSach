using Microsoft.AspNetCore.Mvc;

namespace WebBanSach.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
