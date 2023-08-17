using StackExchange.Redis;

namespace AL.Interpolation.API.DAL.Repositories.Redis
{
    public interface IRedisDatabaseAccessor
    {
        IDatabase GetDatabase(int dbNumber = -1);
        IServer GetServer();
    }
}
