using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.Student
{
    public class CreateStudentDTO
    {
        public string AdmissionNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime EnrollmentDate { get; set; }

        // Relationships (optional update)
        public int ParentId { get; set; }
        public string UserId { get; set; }
        public int ClassId { get; set; }
    }
}
