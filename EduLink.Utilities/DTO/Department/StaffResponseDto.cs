using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.Department
{
    public class StaffResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; } // "Academic" or "Non-Academic"
    }
}
