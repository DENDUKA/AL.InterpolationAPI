using AL.Interpolation.API.DAL.Repositories.Interfaces;
using AL.Interpolation.Entities.Redis;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;

namespace AL.Interpolation.API.DAL.Repositories.Redis
{
    public class RedisIntRequestsRepository : IInterpolationRequestsRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IDatabase _redisDatabase;
        private readonly IServer _redisServer;

        public RedisIntRequestsRepository(
            IDistributedCache distributedCache,
            IRedisDatabaseAccessor redisDatabaseAccessor)
        {
            _distributedCache = distributedCache;
            _redisDatabase = redisDatabaseAccessor.GetDatabase();
            _redisServer = redisDatabaseAccessor.GetServer();
        }

        public async Task Insert(string id, RedisSlotsInfo slotsInfo, CancellationToken token)
        {
            var key = GetKey(id);
            if (await Find(key, token) is null)
            {
                await _distributedCache.SetStringAsync(GetKey(id), JsonSerializer.Serialize(slotsInfo), token);
            }
            else
            {
                throw new ArgumentException($"Запись с таким ключем {key} уже есть в базе");
            }
        }

        public async Task Increment(string id, CancellationToken token)
        {
            var result = await _distributedCache.GetStringAsync(GetKey(id), token);
            if (string.IsNullOrEmpty(result))
            {
                throw new ArgumentException($"Запись с таким ключем {id} нет в базе");
            }

            var slotsInfo = JsonSerializer.Deserialize<RedisSlotsInfo>(result);

            slotsInfo.OcupiedSlots++;

            await _distributedCache.SetStringAsync(GetKey(id), JsonSerializer.Serialize(slotsInfo));
        }

        public async Task Decrement(string id, CancellationToken token)
        {
            var result = await _distributedCache.GetStringAsync(GetKey(id), token);
            if (string.IsNullOrEmpty(result))
            {
                throw new ArgumentException($"Запись с таким ключем {id} нет в базе");
            }

            var slotsInfo = JsonSerializer.Deserialize<RedisSlotsInfo>(result);

            slotsInfo.OcupiedSlots--;

            await _distributedCache.SetStringAsync(GetKey(id), JsonSerializer.Serialize(slotsInfo));
        }

        public async Task<bool> IsExist(string id, CancellationToken token)
        {
            var result = await _distributedCache.GetStringAsync(GetKey(id), token);
            if (string.IsNullOrEmpty(result))
            {
                return false;
            }

            return true;
        }

        public async Task<RedisSlotsInfo?> Find(string id, CancellationToken token)
        {
            var result = await _distributedCache.GetStringAsync(GetKey(id), token);
            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            return JsonSerializer.Deserialize<RedisSlotsInfo>(result);
        }

        public async Task Set(string id, RedisSlotsInfo slotsInfo)
        {
            await _distributedCache.SetStringAsync(GetKey(id), JsonSerializer.Serialize(slotsInfo));
        }

        private static string GetKey(string id) => $"AvalibleSlots : {id}";
    }
}
