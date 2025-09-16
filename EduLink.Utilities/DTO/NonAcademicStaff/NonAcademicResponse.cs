using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.NonAcademicStaff
{
    public class NonAcademicResponse
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string JobTitle { get; set; }
        public string UserId { get; set; }
        public  string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
