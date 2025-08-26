using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.Department
{
    public class CreateDepartmentDTO
    {
        public string DepartmentName { get; set; }

        // UserId or StaffId for the Head of Department
        public int HeadOfDepartmentId { get; set; }

        // Optional: assign staff when creating
        public ICollection<int> AcademicStaffIds { get; set; }
        public ICollection<int> NonAcademicStaffIds { get; set; }
    }
}
