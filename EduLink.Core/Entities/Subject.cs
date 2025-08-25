using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Entities
{
    public class Subject:BaseEntity
    {
        // subject name
        public string  SubjectName { get; set; }
        public int  Code { get; set; }
        //Class 
        public int  ClassId { get; set; }
        public Class Class { get; set; }
        //AcadimcStaff
        public int AcadmicStaffId { get; set; }
        public  AcademicStaff AcademicStaff { get; set; }
    }
}
