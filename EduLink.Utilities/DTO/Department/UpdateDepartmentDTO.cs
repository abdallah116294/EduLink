using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.Department
{
    public class UpdateDepartmentDTO
    {
        public string DepartmentName { get; set; }
        public int HeadOfDepartmentId { get; set; }

        public ICollection<int> AcademicStaffIds { get; set; }
        public ICollection<int> NonAcademicStaffIds { get; set; }
    }
}
