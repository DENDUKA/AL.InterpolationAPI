using AL.Interpolation.API.DAL.Repositories.Redis;

namespace AL.Interpolation.API.Repositories.Redis
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRedis(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("Redis:ConnectionString");

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = connectionString;
            });

            services.AddSingleton<IRedisDatabaseAccessor, RedisDatabaseAccessor>(x =>
            new RedisDatabaseAccessor(connectionString));

            return services;
        }
    }
}