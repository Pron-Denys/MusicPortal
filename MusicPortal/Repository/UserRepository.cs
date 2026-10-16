using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;

namespace MusicPortal.Repository
{
    public class UserRepository(MusicPortalContext _db) : IRepository
    {
        public async Task<object> GetListAsync()
        {
            var users = await _db.Users.AsNoTracking().ToListAsync();
            return users;
        }

        public object FirstOrDefault(object value)
        {
            if (value is int Id)
            {
                var user = _db.Users.FirstOrDefault(u => u.Id == Id)!;
                return user;
            }
            else if (value is string Login)
            {
                var user = _db.Users.FirstOrDefault(u => u.Login == Login)!;
                return user;
            }
            return null!;
        }

        public void Add(object value)
        {
            if (value is User user)
            {
                _db.Add(user);
            }
        }

        public void Update(object value)
        {
            if (value is User user)
            {
                _db.Users.Update(user);
            }
        }

        public void Remove(object value)
        {
            if (value is User user)
            {
                _db.Users.Remove(user);
            }
        }
        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
