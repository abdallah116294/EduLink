using EduLink.Core.IServices.NotificationService;
using EduLink.Service.NotificationService.Hubs;
using EduLink.Utilities.DTO.Attendance;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using EduLink.API.Hubs;

namespace EduLink.Service.NotificationService
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendAttendanceNotificationAsync(string usrID, AttendanceNotificationDto notification)
        {
           await _hubContext.Clients.Group($"user_{usrID}").SendAsync("ReceiveAttendanceNotification", notification);
        }

        public async Task SendAttendanceNotificationToMultipleUsersAsync(List<string> userIds, AttendanceNotificationDto notification)
        {
            var tasks = userIds.Select(userId =>
                _hubContext.Clients
                    .Group($"user_{userId}")
                    .SendAsync("ReceiveAttendanceNotification", notification)
            );

            await Task.WhenAll(tasks);
        }
    }
}
