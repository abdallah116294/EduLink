using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.Student
{
    public class StudentResponseDTO
    {
        public int Id { get; set; }
        public string AdmissionNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime EnrollmentDate { get; set; }
        //Parent Details
        public int ParentId { get; set; }
        public string ParentName { get; set; }
        // User Info (linked identity)
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        // Class
        public int ClassId { get; set; }
        public string ClassName { get; set; }
    }
}
