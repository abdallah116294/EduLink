using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.Attendance
{
    public class AttendanceResponseDTO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string AdmissionNumber { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public bool IsPresent { get; set; }
        public bool NotificationSent { get; set; }
    }
}
