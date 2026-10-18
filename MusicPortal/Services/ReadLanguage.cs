namespace MusicPortal.Services
{
    using MusicPortal.Models;
    public class ReadLanguage : IRead
    {
        public IEnumerable<Language> Languages { get; set; }
        public ReadLanguage(IConfiguration configuration)
        {
            Languages = configuration.GetSection("Lang").GetChildren()
                .Select(l => new Language { Code = l.Key, Name = l.Value });
        }

        public IEnumerable<Language> GetList() => Languages;
    }
}
