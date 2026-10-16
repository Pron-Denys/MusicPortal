namespace MusicPortal.Repository
{
    using MusicPortal.Models;
    public interface IRepository
    {
        Task<object> GetListAsync();
        object FirstOrDefault(object value);
        void Add(object value);
        void Update(object value);
        void Remove(object value);
        void SaveChanges();
    }
}
