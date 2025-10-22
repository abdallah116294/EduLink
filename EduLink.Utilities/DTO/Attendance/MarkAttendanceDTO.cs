using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.Attendance
{
    public class MarkAttendanceDTO
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow;
        [Required]
        public bool IsPresent { get; set; }
    }
}
