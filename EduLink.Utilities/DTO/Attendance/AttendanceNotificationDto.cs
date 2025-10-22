using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.Attendance
{
    public class AttendanceNotificationDto
    {
        public int AttendanceId { get; set; }
        public string StudentName { get; set; }
        public string AdmissionNumber { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public DateTime NotificationTime { get; set; }
        public string RecipientType { get; set; } // "S
    }
}
