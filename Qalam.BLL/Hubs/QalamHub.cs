using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qalam.BLL.Hubs
{
    public class QalamHub : Hub
    {
        private Lazy<IMemoryCache> _cache;
        private static Dictionary<string, int> _groups;

        public QalamHub(Lazy<IMemoryCache> cache)
        {
            _cache = cache;
        }

        private void InitGroups(string groupId = null)
        {
            if (_groups == null)
            {
                _groups = new Dictionary<string, int>();
            }
            if (!_groups.TryGetValue(groupId, out int _))
            {
                _groups.Add(groupId, 0);
            }
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            var httpContext = Context.GetHttpContext();
            string userName = httpContext.Request.Query["user-name"];
            if (!string.IsNullOrEmpty(userName))
            {
                if (!_cache.Value.TryGetValue(userName, out string _))
                {
                    _cache.Value.Set(userName, Context.ConnectionId);
                    Context.Items.Add("user-name", userName);
                }
                else
                {
                    await Clients.Caller.SendAsync("ExistingUser");
                }
            }
            else
            {
                //await Groups.AddToGroupAsync(Context.ConnectionId, "guests");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task LiveStudentMessage(object message, string groupId)
        {
            if (_cache.Value.TryGetValue(groupId + "_user", out string hostConnection))
            {
                await Clients.Client(hostConnection).SendAsync("LiveStudentMessage", message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isCommentsOpen"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public Task LiveTeacherToggleMessage(bool isCommentsOpen, string groupId)
        {
            return Clients.GroupExcept(groupId, Context.ConnectionId).SendAsync("LiveTeacherToggleMessage", isCommentsOpen);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task LiveStudentLike(string userName, string groupId)
        {
            if (_cache.Value.TryGetValue(groupId + "_user", out string hostConnection))
            {
                await Clients.Client(hostConnection).SendAsync("LiveStudentLike", userName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task LiveStudentRaiseHand(string userName, string groupId)
        {
            if (_cache.Value.TryGetValue(groupId + "_user", out string hostConnection))
            {
                await Clients.Client(hostConnection).SendAsync("LiveStudentRaiseHand", userName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task LiveStudentRepeat(string userName, string groupId)
        {
            if (_cache.Value.TryGetValue(groupId + "_user", out string hostConnection))
            {
                await Clients.Client(hostConnection).SendAsync("LiveStudentRepeat", userName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task EndLive(string groupId)
        {
            var isHost = (bool?)Context.Items["host"];
            var userName = (string)Context.Items["user-name"];
            await Clients.GroupExcept(groupId, Context.ConnectionId).SendAsync("LiveEnded");
            await LeaveGroup(groupId, userName, isHost == true);
            Context.Items.Remove("host");
            Context.Items.Remove("group-id");
            _cache.Value.Remove(groupId + "_user");
            _cache.Value.Remove(groupId + "_live");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task JoinGroup(string groupId, string userName, bool isGroupHost, int liveId)
        {
            await Task.Run(() => InitGroups(groupId));

            if (!Context.Items.ContainsKey("host"))
            {
                Context.Items.Add("host", isGroupHost);
            }
            if (!Context.Items.ContainsKey("group-id"))
            {
                Context.Items.Add("group-id", groupId);
                _groups[groupId] += (isGroupHost ? 0 : 1);
            }

            if (isGroupHost)
            {
                _cache.Value.Set(groupId + "_user", Context.ConnectionId);
                _cache.Value.Set(groupId + "_live", liveId);
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
            await Clients.GroupExcept(groupId, Context.ConnectionId).SendAsync("UserJoinGroup", userName, _groups[groupId]);

            if (!_cache.Value.TryGetValue(groupId, out string _))
            {
                await Clients.Client(Context.ConnectionId).SendAsync("NoGroupHost");
            }
        }

        private async Task LeaveGroup(string groupId, string userName, bool isGroupHost)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);

            await Task.Run(() => InitGroups(groupId));
            _groups[groupId] -= (isGroupHost ? 0 : 1);

            await Clients.GroupExcept(groupId, Context.ConnectionId).SendAsync("UserLeaveGroup", userName, isGroupHost, _groups[groupId]);
            if (isGroupHost)
            {
                _cache.Value.Remove(groupId);
            }
        }

        public async Task OnReconnectedAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                //await Groups.AddToGroupAsync(Context.ConnectionId, "guests");
            }
            else
            {
                //await Groups.RemoveFromGroupAsync(Context.ConnectionId, "guests");
                await Task.Run(() => _cache.Value.Set(userName, Context.ConnectionId));
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var groupId = (string)Context.Items["group-id"];
            var isHost = (bool?)Context.Items["host"];
            var userName = (string)Context.Items["user-name"];

            if (!string.IsNullOrEmpty(groupId))
            {
                await LeaveGroup(groupId, userName, isHost == true);
            }

            _cache.Value.Remove(userName);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
