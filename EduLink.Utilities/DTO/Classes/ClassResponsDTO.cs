using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.Classes
{
    public class ClassResponsDTO
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string GradeLevel { get; set; }

        public int AcademicYearId { get; set; }
        public string AcademicYearName { get; set; }
        public ICollection<StudentResponseDto> Students { get; set; }
        public ICollection<SubjectResponseDto> Subjects { get; set; }
        public ICollection<ExamResponseDto> Exams { get; set; }
    }
    public class StudentResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }

    public class SubjectResponseDto
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
    }

    public class ExamResponseDto
    {
        public int Id { get; set; }
        public string ExamName { get; set; }
        public DateTime ExamDate { get; set; }
    }
}
