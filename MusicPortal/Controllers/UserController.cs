using Microsoft.AspNetCore.Mvc;

namespace MusicPortal.Controllers
{
    using System.Text;
    using MusicPortal.Models;
    using System.Security.Cryptography;
    using MusicPortal.Repository;
    public class UserController(IEnumerable<IRepository> repo) : Controller
    {
        public async Task<IActionResult> Index()
        {
            UserRepository? repository = repo.FirstOrDefault(r => r is UserRepository) as UserRepository;
            var users = await repository?.GetListAsync()!;
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RegistrationModel userModel, string? IsAdmin, string? IsActived)
        {
            if (!ModelState.IsValid) return View();
            var saltBytes = RandomNumberGenerator.GetBytes(16);
            var salt = Convert.ToHexString(SHA256.HashData(saltBytes));
            byte[] passwordBytes = Encoding.Unicode.GetBytes(salt + userModel.Password);
            string password = Convert.ToHexString(SHA256.HashData(passwordBytes));
            var user = new User { Name  = userModel.Name, Password = password, IsActivated = (IsActived == null) ? false : true, IsAdmin = (IsAdmin == null) ? false : true, Salt = salt, Login = userModel.Login, Surname = userModel.Surname, Year = userModel.Year };
            UserRepository? repository = repo.FirstOrDefault(r => r is UserRepository) as UserRepository;
            repository?.Add(user);
            repository?.SaveChanges();
            return RedirectToAction("Index", "User");
        }

        public IActionResult Update(int id)
        {
            UserRepository? repository = repo.FirstOrDefault(r => r is UserRepository) as UserRepository;
            var user = repository?.FirstOrDefault(id) as User;
            if (user == null) return NotFound();
            var model = new RegistrationModel { Name = user.Name, Login = user.Login, Surname = user.Surname, Password = "Password", PasswordConfirm = "Password", Year = user.Year };
            ViewBag.Id = user.Id;
            ViewBag.IsAdmin = user.IsAdmin.ToString();
            ViewBag.IsActivated = user.IsActivated.ToString();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(RegistrationModel model, int id, string? isActived, string? isAdmin)
        {
            if (!ModelState.IsValid) return View(model);
            UserRepository? repository = repo.FirstOrDefault(r => r is UserRepository) as UserRepository;
            var user = repository?.FirstOrDefault(id) as User;
            if (user == null) return NotFound();
            if (model.Password != "Password")
            {
                byte[] passwordBytes = Encoding.Unicode.GetBytes(user.Salt + model.Password);
                var password = Convert.ToHexString(SHA256.HashData(passwordBytes));
                user.Password = password;
            }
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Login = model.Login;
            user.Year = model.Year;
            user.IsActivated = (isActived != null) ? true : false;
            user.IsAdmin = (isAdmin != null) ? true : false;
            repository?.Update(user);
            repository?.SaveChanges();
            return RedirectToAction("Index", "User");
        }

        public IActionResult Delete(int id)
        {
            UserRepository? repository = repo.FirstOrDefault(r => r is UserRepository) as UserRepository;
            var user = repository?.FirstOrDefault(id) as User;
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int id)
        {
            UserRepository? repository = repo.FirstOrDefault(r => r is UserRepository) as UserRepository;
            var user = repository?.FirstOrDefault(id) as User;
            if (user == null) return NotFound();
            repository?.Remove(user);
            repository?.SaveChanges();
            return RedirectToAction("Index", "User");
        }
    }
}
