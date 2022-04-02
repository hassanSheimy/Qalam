using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Qalam.BLL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Qalam.BLL.Hubs
{
    public class LiveHandler
    {
        private IHubContext<QalamHub> _hubContext;
        private Lazy<IMemoryCache> _cache;
        private Lazy<LiveStreamManager> _liveVideoManager;
        public LiveHandler(IHubContext<QalamHub> hubContext, Lazy<IMemoryCache> cache, 
            Lazy<LiveStreamManager> liveVideoManager)
        {
            _hubContext = hubContext;
            _cache = cache;
            _liveVideoManager = liveVideoManager;
        }

        public int? GetLiveId(string streamKey)
        {
            try
            {
                if (_cache.Value.TryGetValue(streamKey + "_live", out int liveId))
                {
                    return liveId;
                }
                var result = _liveVideoManager.Value.GetRecentlyLive(streamKey);

                return !result.IsSucceeded || result.Data == null ? null : result.Data;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
