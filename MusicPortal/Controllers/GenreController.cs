using Microsoft.AspNetCore.Mvc;

namespace MusicPortal.Controllers
{
    using Microsoft.EntityFrameworkCore;
    using MusicPortal.Filters;
    using MusicPortal.Models;
    using MusicPortal.Repository;
    [Culture]
    public class GenreController(IEnumerable<IRepository> repo, IWebHostEnvironment app) : Controller
    {
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetString("path", Request.Path);
            GenreRepository? repository = repo.FirstOrDefault(r => r is GenreRepository) as GenreRepository;
            var genres = await repository?.GetListAsync()! as List<Genre>;
            return View(genres);
        }

        public IActionResult Create()
        {
            HttpContext.Session.SetString("path", Request.Path);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(1_000_000_000)]
        public IActionResult Create(Genre genreModel, IFormFile filePoster)
        {
            HttpContext.Session.SetString("path", Request.Path);
            if (!ModelState.IsValid || filePoster == null)
            {
                if (filePoster == null) ModelState.AddModelError("Poster", Resources.Resource.ChoosePoster);
                return View(genreModel);
            }
            var pathPoster = Path.Combine(app.WebRootPath, "Genre Poster", filePoster.FileName);
            FileStream stream = new FileStream(pathPoster, FileMode.Create);
            filePoster.CopyTo(stream);
            stream.Dispose();
            var genre = new Genre { Name = genreModel.Name, Poster = filePoster.FileName };
            GenreRepository? repository = repo.FirstOrDefault(r => r is GenreRepository) as GenreRepository;
            repository?.Add(genre);
            repository?.SaveChanges();
            return Redirect(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            HttpContext.Session.SetString("path", Request.Path);
            GenreRepository? repository = repo.FirstOrDefault(r => r is GenreRepository) as GenreRepository;
            var genre = repository?.FirstOrDefault(id) as Genre;
            if (genre == null) return View();
            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(1_000_000)]
        public IActionResult Update(Genre genreModel, IFormFile filePoster)
        {
            HttpContext.Session.SetString("path", Request.Path);
            if (genreModel.Name == null) return View(genreModel);
            GenreRepository? repository = repo.FirstOrDefault(r => r is GenreRepository) as GenreRepository;
            var genre = repository?.FirstOrDefault(genreModel.Id) as Genre;
            if (genre == null) return NotFound();
            genre.Name = genreModel.Name;
            if (filePoster != null)
            {
                var pathPoster = Path.Combine(app.WebRootPath, "Genre Poster", filePoster.FileName);
                FileStream stream = new FileStream(pathPoster, FileMode.Create);
                filePoster.CopyTo(stream);
                stream.Dispose();
                genre.Poster = filePoster.FileName;
            }
            repository?.Update(genre);
            repository?.SaveChanges();
            return RedirectToAction("Index", "Genre");
        }

        public IActionResult Delete(int id)
        {
            HttpContext.Session.SetString("path", Request.Path);
            GenreRepository? repository = repo.FirstOrDefault(r => r is GenreRepository) as GenreRepository;
            var genre = repository?.FirstOrDefault(id) as Genre;
            return View(genre);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int id)
        {
            GenreRepository? repository = repo.FirstOrDefault(r => r is GenreRepository) as GenreRepository;
            var genre = repository?.FirstOrDefault(id) as Genre;
            if (genre == null) return NotFound();
            repository?.Remove(genre);
            repository?.SaveChanges();
            return RedirectToAction("Index", "Genre");
        }
    }
}
