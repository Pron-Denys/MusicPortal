namespace MusicPortal.Repository
{
    public static class MusicRepositoryExtensions
    {
        public static void AddMusicRepository(this IServiceCollection sevices)
        {
            sevices.AddScoped<IRepository, MusicRepository>();
        }
    }
}
