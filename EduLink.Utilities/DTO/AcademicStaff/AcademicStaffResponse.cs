using EduLink.Utilities.DTO.Classes;
using EduLink.Utilities.DTO.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.AcademicStaff
{
    public class AcademicStaffResponse
    {
        public int Id { get; set; } 
        public int DepartmentId { get; set; }
        public string Specialization { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DepartmentResponseDTO Department { get; set; }
        public ICollection< SubjectResponseDto> Subject { get; set; }
    }
}
