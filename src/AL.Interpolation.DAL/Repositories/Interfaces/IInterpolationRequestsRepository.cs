using AL.Interpolation.Entities.Redis;

namespace AL.Interpolation.API.DAL.Repositories.Interfaces
{
    public interface IInterpolationRequestsRepository
    {
        Task Insert(string id, RedisSlotsInfo redisAvalibleSlots, CancellationToken token);
        Task<bool> IsExist(string id, CancellationToken token);
        Task<RedisSlotsInfo?> Find(string id, CancellationToken token);
        Task Set(string id, RedisSlotsInfo slotsInfo);
        Task Decrement(string id, CancellationToken token);
        Task Increment(string id, CancellationToken token);
    }
}