using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.AcademicYear
{
    public class ClassResponseDTO
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public int GradeLevel { get; set; }   // Example: Grade 1, Grade 2, etc.
    }
}
