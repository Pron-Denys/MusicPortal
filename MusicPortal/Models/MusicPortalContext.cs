namespace MusicPortal.Models
{
    using Microsoft.EntityFrameworkCore;
    public class MusicPortalContext : DbContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Music> Musics { get; set; }
        public DbSet<User> Users { get; set; }

        public MusicPortalContext(DbContextOptions<MusicPortalContext> options)
           : base(options)
        {
            if (Database.EnsureCreated())
            {
                Genre genre_1 = new Genre { Poster= "stock-vector-pop-music-vintage-d-vector-lettering-retro-bold-font-typeface-pop-art-stylized-text-old-school-1427685572.jpg", Name = "Поп-музика" };
                Genre genre_2 = new Genre { Poster= "rap-hip-hop-graffiti-vector-poster-grunge-50774593.webp", Name = "Хіп-Хоп та Реп" };
                Genres?.Add(genre_1);
                Genres?.Add(genre_2);
                Users?.Add(new User { Login = "petrenko24@gmail.com", Name = "Іван", Surname = "Петренко", Password = "ivan245", Salt=" ", Year=24, IsActivated=true, IsAdmin=false });
                Users?.Add(new User { Login = "ivanenko30@gmail.com", Name = "Петро", Surname = "Іваненко", Password = "petro30", Salt = " ", Year = 30, IsActivated = false, IsAdmin = true });
                Users?.Add(new User { Login = "stepanenko22@gmail.com", Name = "Роман", Surname = "Степаненко", Password = "stepan30", Salt = " ", Year = 22, IsActivated = false, IsAdmin = false });
                Musics?.Add(new Music { Name= "Arta Si Precizie (Intro)", Year = 2025, Songwriter= "B.U.G. Mafia", Audio = "B.U.G. Mafia - Arta Si Precizie (Intro).mp3", Poster = "b_u_g_-mafia-arta-si-precizie-intro.webp", Genre = genre_1 });
                Musics?.Add(new Music { Name = "Mai e", Year = 2024, Songwriter = "Carla’s Dreams", Audio = "Carla’s Dreams - Mai e.mp3", Poster = "carlas-dreams-mai-e.webp", Genre = genre_2 });
                Musics?.Add(new Music { Name = "Memories (Souvenirs)", Year = 2023, Songwriter = "Charlotte Cardin", Audio = "Charlotte Cardin - Memories (Souvenirs).mp3", Poster = "charlotte-cardin-memories-souvenirs.webp", Genre = genre_1 });
                Musics?.Add(new Music { Name = "Who Dat Is", Year = 2025, Songwriter = "Young Nudy", Audio = "Young Nudy - Who Dat Is.mp3", Poster = "young-nudy-who-dat-is.webp", Genre = genre_2 });
                SaveChanges();
            }
        }
    }
}
