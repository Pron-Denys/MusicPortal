namespace MusicPortal.Services
{
    public static class ReadLanguageExstensions
    {
        public static void AddReadLanguage(this IServiceCollection services)
        {
            services.AddScoped<IRead,  ReadLanguage>();
        }
    }
}
