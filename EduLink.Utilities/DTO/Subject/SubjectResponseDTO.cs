using EduLink.Utilities.DTO.AcademicStaff;
using EduLink.Utilities.DTO.AcademicYear;
using EduLink.Utilities.DTO.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.Subject
{
    public class SubjectResponseDTO
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public int Code { get; set; }
        //Class 
        public int ClassId { get; set; }
        public ClassResponsDTO Class { get; set; }
        public AcademicStaffResponse AcademicSteff { get; set; }
    }
}
