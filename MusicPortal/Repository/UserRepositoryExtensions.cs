namespace MusicPortal.Repository
{
    public static class UserRepositoryExtensions
    {
        public static void AddUserRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepository, UserRepository>();
        }
    }
}
