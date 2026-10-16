using Microsoft.AspNetCore.Mvc;

namespace MusicPortal.Controllers
{
    using System.Text;
    using MusicPortal.Models;
    using System.Security.Cryptography;
    using MusicPortal.Repository;

    public class AccountController(IEnumerable<IRepository> repo) : Controller
    {
        public IActionResult Index()
        {
            ViewData["Menu"] = "none";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(UserLogin User)
        {
            if (!ModelState.IsValid) return View(User);
            UserRepository? repository = repo.FirstOrDefault(r => r is UserRepository) as UserRepository;
            var user = repository?.FirstOrDefault(User.Login!) as User;
            if (user == null)
            {
                ModelState.AddModelError(String.Empty, "Не правильний логін або пароль");
                return View(User);
            }
            var passwordBytes = Encoding.Unicode.GetBytes(user.Salt + User.Password);
            var password = Convert.ToHexString(SHA256.HashData(passwordBytes));
            if (user.Password != password)
            {
                ModelState.AddModelError(String.Empty, "Не правильний логін або пароль");
                return View(User);
            }
            if (user.IsAdmin == true) HttpContext.Session.SetString("IsAdmin", "true");
            if (user.IsActivated == true) HttpContext.Session.SetString("IsActivated", "true");
            return RedirectToAction("Index", "Music");
        }

        public IActionResult Registration() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(RegistrationModel User)
        {
            if (!ModelState.IsValid) return View(User);
            var saltBytes = RandomNumberGenerator.GetBytes(16);
            var salt = Convert.ToHexString(SHA256.HashData(saltBytes));
            var passwordBytes = Encoding.Unicode.GetBytes(salt + User.Password);
            var password = Convert.ToHexString(SHA256.HashData(passwordBytes));
            bool IsAdmin = false;
            if (HttpContext.Session.GetString("Admin") != null)
                IsAdmin = true;
            var user = new User { Name = User.Name, Surname = User.Surname, Login = User.Login, Password = password, Salt = salt, Year = User.Year, IsActivated = false, IsAdmin = IsAdmin };
            if (IsAdmin == true) HttpContext.Session.SetString("IsAdmin", "true");
            UserRepository? repository = repo.FirstOrDefault(r => r is UserRepository) as UserRepository;
            repository?.Add(user);
            repository?.SaveChanges();
            return RedirectToAction("Index", "Music");
        }

        public IActionResult Exit()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Music");
        }
    }
}
