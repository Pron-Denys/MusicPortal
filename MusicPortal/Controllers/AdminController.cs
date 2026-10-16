using Microsoft.AspNetCore.Mvc;

namespace MusicPortal.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.SetString("Admin", " ");
            return RedirectToAction("Index", "Account");
        }

        public IActionResult ToGoBack()
        {
            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Index", "Music");
        }
    }
}
