using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.Department
{
    public class DepartmentResponseDTO
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }

        public int HeadOfDepartmentId { get; set; }
        public string HeadOfDepartmentName { get; set; }

        public ICollection<StaffResponseDto> AcademicStaff { get; set; }
        public ICollection<StaffResponseDto> NonAcademicStaff { get; set; }
    }
}
