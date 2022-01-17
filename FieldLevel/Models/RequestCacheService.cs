using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace FieldLevel.Models
{
    public interface IRequestCacheService
    {
        Task RefreshCacheAsync();
        void RemoveCacheAsync();
    }
    public class RequestCacheService: IRequestCacheService
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger _logger;

        public RequestCacheService(IDistributedCache cache, ILogger<RequestCacheService> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public async Task RefreshCacheAsync()
        {
            var posts = System.Text.Encoding.UTF8.GetBytes(JsonSerializer.Serialize(Posts.GetCurrentPosts()));

            await _cache.SetAsync("Posts", posts, new DistributedCacheEntryOptions());

            _logger.LogInformation("Request Cache Service cache refreshed.");
        }

        public void RemoveCacheAsync()
        {
            _cache.Remove("Posts");
        }

    }
}
