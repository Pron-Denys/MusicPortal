namespace MusicPortal.Services
{
    using MusicPortal.Models;
    public interface IRead
    {
        IEnumerable<Language> GetList();
    }
}
