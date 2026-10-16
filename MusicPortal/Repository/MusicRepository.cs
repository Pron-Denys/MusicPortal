namespace MusicPortal.Repository
{
    using Microsoft.EntityFrameworkCore;
    using MusicPortal.Models;
    public class MusicRepository(MusicPortalContext _db) : IRepository
    {
        public async Task<object> GetListAsync()
        {
            var musics = await _db.Musics.AsNoTracking().ToListAsync();
            return musics;
        }

        public object FirstOrDefault(object value)
        {
            if (value is int Id)
            {
                var music = _db.Musics.FirstOrDefault(m => m.Id == Id);
                return music!;
            }
            return null!;
        }

        public void Add(object value)
        {
            if (value is Music music)
            {
                _db.Musics.Add(music);
            }
        }

        public void Update(object value)
        {
            if (value is Music music)
            {
                _db.Musics.Update(music);
            }
        }

        public void Remove(object value)
        {
            if (value is Music music)
            {
                _db.Musics.Remove(music);
            }
        }

        public object IncludeToList()
        {
            var musics = _db.Musics.Include(m => m.Genre).ToList();
            return musics;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
