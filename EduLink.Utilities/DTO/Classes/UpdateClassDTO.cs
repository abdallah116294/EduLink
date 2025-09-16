using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.Classes
{
    public class UpdateClassDTO
    {
        public string ClassName { get; set; }
        public string GradeLevel { get; set; }

        public int AcademicYearId { get; set; }

        // Update relationships
        public ICollection<int> StudentIds { get; set; }
        public ICollection<int> SubjectIds { get; set; }
        public ICollection<int> ExamIds { get; set; }
    }
}
