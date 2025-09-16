using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.AcademicStaff
{
    public class CreateAcademicStaffDTO
    {
        public int DepartmentId { get; set; }
        public string Specialization { get; set; }
        public string UserId { get; set; }

    }
}
