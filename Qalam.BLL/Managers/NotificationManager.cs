using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Qalam.BLL.Hubs;
using Qalam.BLL.ViewModels;
using Qalam.MYSQL.Repository;
using Qalam.MYSQL.Entities;
using Qalam.MYSQL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Qalam.Common.Enums;
using Qalam.Common.Extensions;

namespace Qalam.BLL.Managers
{
    public class NotificationManager : Repository<Notification>
    {
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _services;
        public NotificationManager(QalamDBContext dBContext, IMapper mapper,
            IServiceScopeFactory services) : base(dBContext)
        {
            _mapper = mapper;
            _services = services;
        }

        public Task ReadNotificationAsync(List<int> notificationIds)
        {
            throw new NotImplementedException();
        }

        public async Task SendLiveStreamNotifications(LiveStreamViewModel liveStreamViewModel)
        {
            try
            {
                using IServiceScope scope = _services.CreateScope();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager>();
                var notificationHandler = scope.ServiceProvider.GetRequiredService<NotificationHandler>();
                var notificationManager = scope.ServiceProvider.GetRequiredService<NotificationManager>();

                var teacher = userManager.Get(u => u.Teacher.Id == liveStreamViewModel.TeacherId,
                    new string[] { "Files", "Teacher" });

                if (teacher == null)
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                var users = userManager.GetAll(u => u.Student.FollowingTeachers.Any(t => t.Teacher.Subject.Courses
                    .FirstOrDefault(c => c.Id == liveStreamViewModel.CourseId).EducationYearId == u.Student.EducationYearId));

                if (users == null)
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                var notifications = new List<Notification>();
                foreach (var user in users)
                {
                    var notification = new NotificationViewModel
                    {
                        IsRead = false,
                        Message = "",
                        ReleaseDate = DateTime.UtcNow,
                        Url = "/live/watch/" + teacher.Teacher.StreamKey,
                        UserId = user.Id,
                        SenderId = teacher.Id,
                        Sender = _mapper.Map<NotificationSenderViewModel>(teacher)
                    };
                    notifications.Add(_mapper.Map<Notification>(notification));

                    await notificationHandler.SendNotificationAsync(user.Email, notification);
                }

                var notificationResult = notificationManager.Add(notifications);

                if (notificationResult == null)
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                if (!notificationManager.SaveChanges())
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
