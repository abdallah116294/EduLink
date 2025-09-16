using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.Student
{
    public class AttendanceResponsForSpecifcStudent
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
