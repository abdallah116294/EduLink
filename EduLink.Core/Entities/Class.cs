using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Entities
{
    public class Class:BaseEntity
    {
        public string  ClassName { get; set; }
        public string GradeLevel { get; set; }
        public int AcademicYearId { get; set; }
        public AcadmicYear AcadmicYear { get; set; }
        //has many Students
        public ICollection<Student> Students { get; set; }
        //Has many Subject 
        public ICollection<Subject> Subject { get; set; }
        //Has many Exam
        public ICollection<Exam> Exam { get; set; }
    }
}
