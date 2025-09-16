using EduLink.Utilities.DTO.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.Attendance
{
    public class AttendanceForSpecificClass
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public DateTime Date { get; set; }
        public List<AttendanceResponsForSpecifcStudent> StudentStatuses { get; set; }
    }
}
