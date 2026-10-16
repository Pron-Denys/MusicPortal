using Microsoft.AspNetCore.Mvc;

namespace MusicPortal.Controllers
{
    using Microsoft.EntityFrameworkCore;
    using MusicPortal.Models;
    using MusicPortal.Repository;
    public class MusicController(IEnumerable<IRepository> repo, IWebHostEnvironment app) : Controller
    {
        public async Task<IActionResult> Index()
        {
            MusicRepository? repository = repo.FirstOrDefault(r => r is MusicRepository) as MusicRepository;
            return View(await repository?.GetListAsync()! as IEnumerable<Music>);
        }

        public async Task<IActionResult> Create()
        {
            GenreRepository? repository = repo.FirstOrDefault(r => r is GenreRepository) as GenreRepository;
            ViewBag.Genres = await repository?.GetListAsync()! as IEnumerable<Genre>;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(1_000_000_000)]
        public async Task<IActionResult> Create(MusicModel musicModel, IFormFile fileAudio, IFormFile filePoster)
        {
            GenreRepository? repository = repo.FirstOrDefault(r => r is GenreRepository) as GenreRepository;
            if (!ModelState.IsValid || fileAudio == null || filePoster == null)
            {
                ViewBag.Genres = await repository?.GetListAsync()! as IEnumerator<Genre>;
                if (fileAudio == null) ModelState.AddModelError("Audio", "Оберіть пісню");
                if (filePoster == null) ModelState.AddModelError("Poster", "Оберіть пісню");
                return View(musicModel);
            }
            Genre? genre = repository!.FirstOrDefault(musicModel.Genre!) as Genre;
            var pathAudio = Path.Combine(app.WebRootPath, "Music", fileAudio!.FileName);
            FileStream stream = new FileStream(pathAudio, FileMode.Create);
            fileAudio.CopyTo(stream);
            stream.Dispose();
            var pathPoster = Path.Combine(app.WebRootPath, "Poster", filePoster!.FileName);
            stream = new FileStream(pathPoster, FileMode.Create);
            filePoster!.CopyTo(stream);
            stream.Dispose();
            var music = new Music { Name = musicModel.Name, Songwriter = musicModel.Songwriter, Audio = fileAudio.FileName, Poster = filePoster.FileName, Genre = genre, Year = musicModel.Year };
            MusicRepository? musicRepository = repo.FirstOrDefault(r => r is MusicRepository) as MusicRepository;
            musicRepository?.Add(music);
            musicRepository?.SaveChanges();
            return RedirectToAction("Index", "Music");
        }

        public async Task<IActionResult> Update(int id)
        {
            MusicRepository? repository = repo.FirstOrDefault(r => r is MusicRepository) as MusicRepository;
            var musics = repository?.IncludeToList() as List<Music>;
            Music music = null!;
            foreach (var m in musics!)
            {
                if (m.Id == id) music = m;
            }
            if (music == null) return RedirectToAction("Index", "Music");
            MusicModel model = new MusicModel { Id = music.Id, Name = music.Name, Genre = music?.Genre?.Name, Songwriter = music?.Songwriter, Year = music!.Year };
            GenreRepository? genreRepository = repo.FirstOrDefault(r => r is GenreRepository) as GenreRepository;
            ViewBag.Genres = await genreRepository?.GetListAsync()! as IEnumerable<Genre>;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(1_000_000_000)]
        public async Task<IActionResult> Update(MusicModel musicModel, IFormFile fileAudio, IFormFile filePoster)
        {
            GenreRepository? genreRepository = repo.FirstOrDefault(r => r is GenreRepository) as GenreRepository;
            if (musicModel.Songwriter == null || musicModel.Year == 0 || musicModel.Genre == null || musicModel.Name == null)
            {
                ViewBag.Genres = await genreRepository?.GetListAsync()! as IEnumerable<Genre>;
                return View(musicModel);
            }
            string audio = null!;
            string poster = null!;
            MusicRepository? repository = repo.FirstOrDefault(r => r is MusicRepository) as MusicRepository;
            if (fileAudio == null || filePoster == null)
            {
                var temp_music = repository!.FirstOrDefault(musicModel.Id) as Music;
                if (fileAudio == null) audio = temp_music?.Audio!;
                if (filePoster == null) poster = temp_music?.Poster!;
            }
            var musics = repository?.IncludeToList() as List<Music>;
            Music music = null!;
            foreach (var m in musics!)
            {
                if (m.Id == musicModel.Id) music = m;
            }
            if (music == null) return RedirectToAction("Index", "Music");
            Genre? genre = genreRepository?.FirstOrDefault(musicModel.Genre)! as Genre;
            if (fileAudio != null)
            {
                var pathAudio = Path.Combine(app.WebRootPath, "Music", fileAudio!.FileName);
                FileStream stream = new FileStream(pathAudio, FileMode.Create);
                fileAudio.CopyTo(stream);
                stream.Dispose();
                music.Poster = fileAudio.FileName;
            }
            if (filePoster != null)
            {
                var pathPoster = Path.Combine(app.WebRootPath, "Poster", filePoster!.FileName);
                FileStream stream = new FileStream(pathPoster, FileMode.Create);
                filePoster!.CopyTo(stream);
                stream.Dispose();
                music.Audio = filePoster.FileName;
            }         
            music.Name = musicModel.Name;
            music.Year = musicModel.Year;
            music.Genre = genre;
            repository?.Update(music);
            repository?.SaveChanges();
            return RedirectToAction("Index", "Music");
        }

        public IActionResult Delete(int id)
        {
            MusicRepository? repository = repo.FirstOrDefault(r => r is MusicRepository) as MusicRepository;
            var music = repository?.FirstOrDefault(id) as Music;
            return View(music);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int id)
        {
            MusicRepository? repository = repo.FirstOrDefault(r => r is MusicRepository) as MusicRepository;
            var music = repository?.FirstOrDefault(id) as Music;
            repository?.Remove(music!);
            repository?.SaveChanges();
            return RedirectToAction("Index", "Music");
        }
    }
}
