using Microsoft.Extensions.Caching.Memory;
using URLShortener.Abstraction;

namespace URLShortener.Services
{
    public class MemoryCachRepo : IMemoryCachRepo
    {

        private readonly IMemoryCache _memoryCache;
        public MemoryCachRepo(IMemoryCache memoryCache)
        {
                _memoryCache = memoryCache;
        }
        public string Get(string key)
        {
            if (_memoryCache.TryGetValue(key, out string? cachedData))
            {
                return cachedData;
            }
            else
            {
                return null;
            }
        }

        public string Set(string key, string value)
        {
            _memoryCache.Set(key, value); // Cache the data for 10 minutes

            return $"Value '{value}' cached successfully with key '{key}'";
        }
    }
}
