using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.Subject
{
    public class CreateSubjectDTO
    {
        public string SubjectName { get; set; }
        public int ? Code { get; set; }
        //Class 
        public int ClassId { get; set; }
        public int AcadmicStaffId { get; set; }
    }
}
