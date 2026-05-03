using eLearning.Models.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DMMS.WebMVC.App.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserLogin userLogin)
        {
            if (userLogin.Email.ToLower() == "ninad.gawankar@gmail.com" && userLogin.PasswordHash == "VCSD@20200")
            {
                return RedirectToAction("Index", "Websites", new { companyIdentity = "8d81559afc6549a38635b9e983722638" });
            }
            if (userLogin.Email.ToLower() == "ninad.gawankar@gmail.com" && userLogin.PasswordHash == "8080")
            {
                return RedirectToAction("Index", "Websites", new { companyIdentity = "da3ffedb31914cea8070c900f7ae37bb" });
            }
            if (userLogin.Email.ToLower() == "ninad.gawankar@adlaabh.com" && userLogin.PasswordHash == "8080")
            {
                return RedirectToAction("Index", "Websites", new { companyIdentity = "184a261ff4944f0a9470ddfff292939f" });
            }
            if (userLogin.Email.ToLower() == "ninad.gawankar@catalystweb.com" && userLogin.PasswordHash == "8080")
            {
                return RedirectToAction("Index", "Websites", new { companyIdentity = "50563356604b48d2b53521b5a7f59337" });
            }
            return View();
        }
        public IActionResult ForgotUsername()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgotUsername(UserLogin userLogin)
        {
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPassword(UserLogin userLogin)
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserLogin userLogin)
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(UserLogin userLogin)
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(UserLogin userLogin)
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
