namespace MusicPortal.Repository
{
    public static class GenreRepositoryExtensions
    {
        public static void AddGenreRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepository,  GenreRepository>();
        }
    }
}
