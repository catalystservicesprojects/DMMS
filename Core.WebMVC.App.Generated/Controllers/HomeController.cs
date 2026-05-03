using Microsoft.AspNetCore.Mvc;

namespace DMMS.WebMVC.App.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.SetString("Name", "Ninad Gawankar");
            HttpContext.Session.SetInt32("CompanyId", 9);
            HttpContext.Session.SetInt32("UserId", 1);

            return View();
        }
    }
}
