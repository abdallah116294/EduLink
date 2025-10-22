using EduLink.Utilities.DTO.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.IServices.NotificationService
{
    public interface INotificationService
    {
        Task SendAttendanceNotificationAsync(string usrID, AttendanceNotificationDto notification);
        Task SendAttendanceNotificationToMultipleUsersAsync(List<string> userIds, AttendanceNotificationDto notification);
    }
}
