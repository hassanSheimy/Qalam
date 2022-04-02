using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Qalam.BLL.Managers;
using Qalam.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qalam.BLL.Hubs
{

    public class NotificationHandler
    {
        private IHubContext<QalamHub> _hubContext;
        private Lazy<IMemoryCache> _cache;
        private Lazy<NotificationManager> _notificationManager;
        public NotificationHandler(IHubContext<QalamHub> hubContext,
            Lazy<IMemoryCache> cache,
            Lazy<NotificationManager> notificationManager)
        {
            _hubContext = hubContext;
            _cache = cache;
            _notificationManager = notificationManager;
        }
        public async Task ReadNotificationAsync(List<int> notificationIds)
        {
            await _notificationManager.Value.ReadNotificationAsync(notificationIds);
        }

        public async Task SendNotificationAsync(string userName, params NotificationViewModel[] notifications)
        {
            try
            {
                if (_cache.Value.TryGetValue(userName, out string connectionId))
                {
                    await _hubContext.Clients.Client(connectionId).SendAsync("Notifications", notifications);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
